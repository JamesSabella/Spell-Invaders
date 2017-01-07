using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GUIScreen_S : MonoBehaviour
{
    //Public Var
    public GUIText scoreText; 				//GUI Score
    public GUIText GameOverText; 			//GUI GameOver
    public GUIText FinalScoreText; 			//GUI Final Score
    public GUIText ReplayText; 				//GUI Replay
    public static int score = 0; 			//Total in-game Score
    public static bool gameover = false; 	//GameOver Trigger
    public static bool levelComplete = false;
    public GameObject player;
    public GameObject returnButton;
    public int count = 0;

    // Use this for initialization
    void Start()
    {
        gameover = false; 					//return the Gameover trigger to its initial state when the game restart
        levelComplete = false;
        //score = 0; 							//return the Score to its initial state when the game restart
    }

    // Fixed Update is called one per specific time
    void FixedUpdate()
    {
        scoreText.text = "Score: " + score; 			//Update the GUI Score

        //Excute when the GameOver Trigger is True
        if (gameover == true)
        {
            GameOverText.text = "GAME OVER"; 			//Show GUI GameOver
            FinalScoreText.text = "" + score; 			//Show GUI FinalScore
            ReplayText.text = "PRESS R TO REPLAY"; 		//Show GUI Replay
            returnButton.SetActive(true);

            if (Input.GetKey("r"))
            {
                SceneManager.LoadScene(1);
                score = 0;
            }

        }

        if (levelComplete == true)
        {
            GameOverText.text = "COMPLETE";
            ReplayText.text = "Click to Continue";

            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}

  