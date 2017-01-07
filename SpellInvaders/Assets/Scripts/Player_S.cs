using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_S : MonoBehaviour {
    public GameObject player;
    Vector3 spawn = new Vector3(0, -4, 0);
    public float speed;
    public Boundary boundary;
    public GameObject laser;	
    public Transform laserSpawn;
    public GameObject shield;
    public static int count = 0;
    public TextMesh move;
    public TextMesh shoot;
    public GameObject Explosion;
    public int playerLives = 3;
    public GameObject livesUI;
    public static float moveHorizontal;
    public static Vector2 movement;
    public static bool moveDown;
    private string highScore;
    public bool hasMoved;

    void Start () {

        shield.SetActive(false);                                        //Shield starts inactive
        moveDown = true;                                                //Used for Ending Animation
        if (Options.rememberedLives!=0)
        playerLives = Options.rememberedLives;
        for (int i = 1; i < playerLives + 1; i++)                       //Spawns UI for lives
        {
            Vector3 spawnUI = new Vector3(8.5F - (i / 2f), -4.5f, 0);               //Sets location for UI to spawn
            var lifename = Instantiate(livesUI, spawnUI, Quaternion.identity);      //Instantiates UI
            lifename.name = ("life" + i);                                           //Names UI in Hierarchy
        }
    }


    void Update() {

        if (Enemy_S.levelComplete == false)                                     //Allows player to move while level is uncomplete
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            movement = new Vector2(moveHorizontal, 0);
            GetComponent<Rigidbody2D>().velocity = movement * speed;
        }

        else

        {
            GameManager_S.levelNumber++;
            Options.rememberedLives=playerLives;
            endingAnimation();                                                  //Player cannot move and flies upward
        }

        //Lock the position in the screen by putting a boundaries
        GetComponent<Rigidbody2D>().position = new Vector2
            (
                Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(GetComponent<Rigidbody2D>().position.y, boundary.yMin, boundary.yMax)
            );

            if (Input.GetKeyDown("space"))                                          //Shoot lasers with space
            {
                Instantiate(laser, laserSpawn.position, Quaternion.identity);       //Instantiates lasers
                GetComponent<AudioSource>().Play();                                 //Plays laser sound
                Destroy(shoot);                                                     //Destroys shoot instructions
            }
        if (GameManager_S.levelNumber == 0)
        {
            if (moveHorizontal > 0.0f || moveHorizontal < 0.0f)                      //Destroys move instructions when player moves
            {
                Destroy(move);
            }
        }
        else
        {
            Destroy(move);
            Destroy(shoot);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyShot")                                           //If player is hit, perform below actions
        {
            var destroyLife = GameObject.Find("life" + playerLives);            //remove life UI
            playerLives--;                                                      //decrement life count
            Instantiate(Explosion, transform.position, transform.rotation);     //Instatiate explosion
            Destroy(destroyLife);                                               
            StartCoroutine(Dead());                                             //Respawn process
            if (playerLives < 0)
            {
                Instantiate(Explosion, transform.position, transform.rotation); 				//Instantiate Explosion
                GUIScreen_S.gameover = true; 											//Trigger That its a GameOver
                Destroy(gameObject);                                                  //Destroy Player Ship Object

                if (GUIScreen_S.score > PlayerPrefs.GetInt("highScore"))
                {
                    PlayerPrefs.SetInt("highScore", GUIScreen_S.score);
                }

            }
        }

        if (other.tag == "PowerUp_Shield")
        {
            shield.SetActive(true);
        }

        /*else if(other.tag == "PowerUp_Bolt")
        {
            for(int i = 0; i < GameManager_S.onscreenShips; i++)
            {
                var enemy = GameObject.Find("enemyClone" + i);
                for (int j = 0; j < GameManager_S.onscreenShips; j++)
                {
                    if (enemy.GetComponentInChildren<TextMesh>().ToString() != GameManager_S.levelWord[j].ToString())
                    {
                        Destroy(enemy);
                    }
                }
            }
        }*/
    }

    void endingAnimation()
    {
        shield.SetActive(false);
        Player_S.moveHorizontal = 0;
        Player_S.movement = new Vector2(0,0);
        GetComponent<Rigidbody2D>().velocity = movement * 0;
        boundary.yMin = -7;
        boundary.yMax = 20;
        if (GetComponent<Rigidbody2D>().position.y > boundary.yMin && moveDown == true)
            transform.Translate(Vector2.down * 5 * Time.deltaTime);

        if (GetComponent<Rigidbody2D>().position.y == boundary.yMin)
            moveDown = false;

        if (moveDown == false)
            transform.Translate(Vector2.up * 15 * Time.deltaTime);
    }

    IEnumerator Dead()
    {
        //move player offscreen
        Debug.Log("dead");
        boundary.xMax = 999;
        transform.position = new Vector3(999, -4, 0);

        //move player back to spawn poition
        yield return new WaitForSeconds(2);
        Debug.Log("respawn");
        transform.position = spawn;
        boundary.xMax = 8;

        //make player invincible
        player.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(2);
        player.GetComponent<BoxCollider2D>().enabled = true;
    }

}
