using UnityEngine;
using System.Collections;

public class Enemy_S : MonoBehaviour {

    //Public Var
    public float speed; //Enemy Ship Speed
    public int health; //Enemy Ship Health
    public GameObject Explosion; //Explosion Prefab
    public int ScoreValue; //How much the Enemy Ship give score after explosion
    public int WrongShipScoreValue; //How much the Enemy Ship give score after explosion
    public Boundary boundary;
    public static int count;
    public GameObject LaserGreenHit;
    public static bool levelComplete;
    public GameObject laser;
    public Transform laserSpawn;
    public GameObject enemy;
    public bool increase;
    

    // Use this for initialization
    void Start()
    {
        if (GameManager_S.levelNumber != 0)
            Destroy(GameObject.Find("/Directions/Enemy"));
        count = 0;
        levelComplete = false;
    }
 

    void Update(){

        //Lock the position in the screen by putting a boundaries
        GetComponent<Rigidbody2D>().position = new Vector2
            (
                Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(GetComponent<Rigidbody2D>().position.y, boundary.yMin, boundary.yMax)
            );
        
        if(GetComponent<Rigidbody2D>().position.y == boundary.yMax && GetComponent<Rigidbody2D>().position.x > boundary.xMin)
            transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (GetComponent<Rigidbody2D>().position.x == boundary.xMin && GetComponent<Rigidbody2D>().position.y > boundary.yMin)
            transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (GetComponent<Rigidbody2D>().position.y == boundary.yMin && GetComponent<Rigidbody2D>().position.x < boundary.xMax)
            transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (GetComponent<Rigidbody2D>().position.x == boundary.xMax && GetComponent<Rigidbody2D>().position.y < boundary.yMax)
            transform.Translate(Vector2.up * speed * Time.deltaTime);

        if (levelComplete == true)
            destroyAllShips();

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        TextMesh tm = gameObject.GetComponentInChildren<TextMesh>();

        //Excute if the object tag was equal to one of these
        if (other.tag == "PlayerLaser")
        {
            Instantiate(LaserGreenHit, transform.position, transform.rotation);             //Instantiate LaserGreenHit 
            Destroy(other.gameObject);                                                      //Destroy the Other (PlayerLaser)                                           

            string temp;
            temp = (GameManager_S.levelWord[count].ToString());
            temp = temp.ToUpper();


            if (temp == tm.text)
            {

                Instantiate(Explosion, transform.position, transform.rotation);             //Instantiate Explosion
                GUIScreen_S.score += ScoreValue;                                    //Increment score by ScoreValue
                var childID = this.transform.FindChild("ID").gameObject;
                Destroy(gameObject);
                if (GameManager_S.levelNumber == 0)
                Destroy(GameObject.Find("/Directions/Enemy"));

                if (count == GameManager_S.levelWord.Length - 1)
                {
                    count++;
                    GUIScreen_S.levelComplete = true;
                    GameObject.Find("/Main Camera/DisplayLetters/letter" + count.ToString()).GetComponent<GUIText>().text = temp;
                    count = -1;   
                    levelComplete = true;
                    Debug.Log("level is complete");
                  
                }

                //Debug.Log("next letter: " + GameManager_S.levelWord[count]);
                else {
                    count++;
                    GameObject.Find("/Main Camera/DisplayLetters/letter" + count.ToString()).GetComponent<GUIText>().text = temp;
                }

            }
            else
            {
                GameObject parent = gameObject;
                GameObject childLetter = parent.transform.FindChild("Letter").gameObject;
                GUIScreen_S.score -= WrongShipScoreValue;
                childLetter.GetComponent<AudioSource>().Play();
                Debug.Log("wrong letter, Hit this letter " + GameManager_S.levelWord[count]);
            }
        }
    }

      public void destroyAllShips()
    {

        Instantiate(Explosion, transform.position, transform.rotation);             //Instantiate Explosion
        GUIScreen_S.score += ScoreValue;                                    //Increment score by ScoreValue
        Destroy(gameObject);
    }

    public void shoot()
    {
        Instantiate(laser, enemy.transform.position, Quaternion.identity);
        GetComponent<AudioSource>().Play();
    }

}
