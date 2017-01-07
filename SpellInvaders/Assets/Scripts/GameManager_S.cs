using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager_S : MonoBehaviour {
    public Sprite redShip;
    public Sprite blueShip;
    public Sprite greenShip;
    public GameObject enemy;
    public Vector2 spawnValues;
    public static int shipcount=8;
    public static string levelWord;
    public static int wordID; 
    public static char[] wordArray;
    public int spriteID;
    public static int onscreenShips = 0;
    public GameObject ufo;
    public float spawnTime = 5f;
    public static int wordIDIndex;
    private float fireRate = 1.5F;
    public static int[] explodedShips;
    public static int explodedShipsCount;
    public int ufoCount;
    public static int levelNumber;

    public static List<string[]> wordList = new List<string[]>()
    {
            new string[] { "W", "E", "A", "T", "H", "E", "R", "H" },
            new string[] { "T", "H", "E", "R", "E", "I", "Y", "A" },
            new string[] { "A", "F", "F", "E", "C", "T", "E", "I" },
            new string[] { "B", "Y", "E", "U", "Y", "E" },
            new string[] { "A", "L", "L", "O", "W", "E", "D", "U" },
            new string[] { "F", "L", "O", "W", "E", "R", "U", "A" },
            new string[] { "B", "L", "E", "W", "U", "E" },
            new string[] { "B", "E", "A", "R" },
            new string[] { "B", "R", "A", "K", "E", "E" },
            new string[] {"C", "O", "U", "R", "S", "E", "A", "S" },
            new string[] {"W", "R", "I", "T", "E", "H", "G", "A" },
            new string[] {"P", "R", "I","N", "C", "I","P", "A", "L", "E" },

    };


    public static List<string[]> differentWords = new List<string[]>()
    {
        new string[] {"weather","whether"},
        new string[] {"theyre","there", "their"},
        new string[] {"affect","effect"},
        new string[] {"by","bye","buy"},
        new string[] {"aloud","allowed"},
        new string[] {"flower","flour"},
        new string[] {"blue","blew"},
        new string[] {"bare","bear"},
        new string[] {"brake","break"},
        new string[] {"course","coarse"},
        new string[] {"write","right"},
        new string[] {"principal","principle"},
    };
    
    void Start () {

        Debug.Log("level: " + levelNumber);
        Time.timeScale =  0;
        explodedShips = new int[10];
        for (int i = 0; i < 10; i++)
            explodedShips[i] = -1;
        ufoCount = 0;
        wordID = Random.Range(0, 12);
        spawn();
        Debug.Log(" Word to Spell: " + levelWord);
        Debug.Log(" Shipcount: " + shipcount);
        Debug.Log("Length of Word to Spell: " + GameManager_S.levelWord.Length);
        InvokeRepeating("SpawnUfo", spawnTime, spawnTime + spawnTime);
        explodedShipsCount = 0;

    }
	
	// Update is called once per frame
	void Update () {

        if (Enemy_S.levelComplete == false)
        {
            fireRate -= Time.deltaTime;
            if (fireRate < 0)
            {
                shipToShoot();
                fireRate = 1.5F;
            }
        }
    }


    void spawn() //Spawns Ships
    {

        chooseWord(); //Chooses variant of word

        setShipcount(); //Sets amount of ships according to word chosen

        spawnLoop(); //Spawns ships

    }

    void spriteChanger()
    {
        SpriteRenderer shipSprite = enemy.GetComponentInChildren<SpriteRenderer>();

        spriteID = Random.Range(1, 4);

        if (spriteID == 1)
            shipSprite.sprite = blueShip;
        else if (spriteID == 2)
            shipSprite.sprite = redShip;
        else if (spriteID == 3)
            shipSprite.sprite = greenShip;

    }

    void setShipcount()
    {

        if (wordID == 3 | wordID == 6 | wordID == 8)
            shipcount = 6;
        else if (wordID == 7)
            shipcount = 4;
        else if (wordID == 11)
            shipcount = 10;
        else
            shipcount = 8;

    }

    void spawnLoop()
    {
        GameObject parent = enemy;
        GameObject childLetter = parent.transform.FindChild("Letter").gameObject;

        TextMesh tm = enemy.GetComponentInChildren<TextMesh>();

        onscreenShips = 0;

        reshuffle(wordList[wordID]);


        //NEEDS TO BE AN ALGORITHM
        if (shipcount == 6) 
            spawnValues.x = 5;
        else if (shipcount == 8)
            spawnValues.x = 6;
        else if (shipcount == 10)
            spawnValues.x = 6;
        else if (shipcount == 4)
            spawnValues.x = 4;



            for (int i = 0; i < shipcount; i++)
        {

            tm.text = wordList[wordID][i];
            spriteChanger();

            if (onscreenShips == 0)
            {

                Vector2 spawnPosition = new Vector2(spawnValues.x, spawnValues.y);
                Quaternion spawnRotation = Quaternion.identity;
                var enemyClone = Instantiate(enemy, spawnPosition, spawnRotation);
                enemyClone.name = ("enemyClone" + onscreenShips);
                GameObject.Find("enemyClone" + onscreenShips + "/ID").GetComponentInChildren<TextMesh>().text = onscreenShips.ToString();
                onscreenShips++;

            }
            else if (onscreenShips > 0 && onscreenShips < shipcount/2)
            {

                Vector2 spawnPosition = new Vector2(spawnValues.x -= 16/(shipcount/2), spawnValues.y);
                Quaternion spawnRotation = Quaternion.identity;
                var enemyClone = Instantiate(enemy, spawnPosition, spawnRotation);
                enemyClone.name = ("enemyClone" + onscreenShips);
                GameObject.Find("enemyClone" + onscreenShips + "/ID").GetComponentInChildren<TextMesh>().text = onscreenShips.ToString();
                onscreenShips++;

            }

            else if (onscreenShips == shipcount / 2)
            {

                Vector2 spawnPosition = new Vector2(spawnValues.x, spawnValues.y - 2);
                Quaternion spawnRotation = Quaternion.identity;
                var enemyClone = Instantiate(enemy, spawnPosition, spawnRotation);
                enemyClone.name = ("enemyClone" + onscreenShips);
                GameObject.Find("enemyClone" + onscreenShips + "/ID").GetComponentInChildren<TextMesh>().text = onscreenShips.ToString();
                onscreenShips++;

            }

            else if (onscreenShips > shipcount / 2 && onscreenShips < shipcount)
            {

                Vector2 spawnPosition = new Vector2(spawnValues.x += 16/(shipcount/2), spawnValues.y - 2);
                Quaternion spawnRotation = Quaternion.identity;
                var enemyClone = Instantiate(enemy, spawnPosition, spawnRotation);
                enemyClone.name = ("enemyClone" + onscreenShips);
                GameObject.Find("enemyClone" + onscreenShips + "/ID").GetComponentInChildren<TextMesh>().text = onscreenShips.ToString();
                onscreenShips++;

            }
        }
    }

    void chooseWord()
    {
        if (wordID == 1 | wordID == 3)
        {
            wordIDIndex = Random.Range(0, 3);
            levelWord = differentWords[wordID][wordIDIndex];
        }
        else
        {
            wordIDIndex = Random.Range(0, 2);
            levelWord = differentWords[wordID][wordIDIndex];
        }

    }

    void reshuffle(string[] texts)
    {
        //Knuth shuffle algorithm
        for (int t = 0; t < texts.Length; t++)
        {
            string tmp = texts[t];
            int r = Random.Range(t, texts.Length);
            texts[t] = texts[r];
            texts[r] = tmp;
        }
    }

    void SpawnUfo()
    {;

        if (ufoCount < 2) {
            if (Enemy_S.levelComplete == false)
            {
                Vector2 spawnPosition = new Vector2(10f, 4.3f);
                Quaternion spawnRotation = Quaternion.identity;
                var newUfo = GameObject.Instantiate(ufo, spawnPosition, spawnRotation);
                ufoCount++;
            }
        }
    }

    void shipToShoot()
    {
         int temp;
         temp = Random.Range(0, onscreenShips);
         var shooter = GameObject.Find("enemyClone" + temp);
         shooter.GetComponent<Enemy_S>().shoot();
    }
}

