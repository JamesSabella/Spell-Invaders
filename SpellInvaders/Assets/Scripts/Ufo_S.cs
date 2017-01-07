using UnityEngine;
using System.Collections;

public class Ufo_S : MonoBehaviour {
    public GameObject ufo;
    public Sprite redufo;
    public Sprite blueufo;
    public Sprite greenufo;
    public Sprite yellowufo;
    public GameObject bolt;
    public GameObject shield;
    public float speed;
    public int health;
    public GameObject LaserGreenHit;
    public GameObject Explosion;
    public int ScoreValue;

    // Use this for initialization
    void Start()
    {
        spriteChanger();
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (Enemy_S.levelComplete == true)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(ufo);
        }
        
    }

    void spriteChanger()
    {
        int spriteID;
        SpriteRenderer shipSprite = ufo.GetComponentInChildren<SpriteRenderer>();

        spriteID = Random.Range(1, 4);

        if (spriteID == 1)
            shipSprite.sprite = blueufo;
        else if (spriteID == 2)
            shipSprite.sprite = redufo;
        else if (spriteID == 3)
            shipSprite.sprite = greenufo;
        else if (spriteID == 4)
            shipSprite.sprite = yellowufo;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Excute if the object tag was equal to one of these
        if (other.tag == "PlayerLaser")
        {
            Instantiate(LaserGreenHit, transform.position, transform.rotation);
            Destroy(other.gameObject);

            //Check the Health if greater than 0
            if (health > 0)
                health--;

            //Check the Health if less or equal 0
            if (health <= 0)
            {
                Instantiate(Explosion, transform.position, transform.rotation);
                GUIScreen_S.score += ScoreValue;
                spawnPowerup();
                Destroy(gameObject);
            }
        }
    }

    void spawnPowerup()
    {
        /*int powerID;
        powerID = Random.Range(1, 3);

        if (powerID == 1)
            Instantiate(bolt, transform.position, transform.rotation);
        else if (powerID == 2)
            Instantiate(sheild, transform.position, transform.rotation);*/
        Instantiate(shield, transform.position, transform.rotation);
    }

}
