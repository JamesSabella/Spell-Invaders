using UnityEngine;
using System.Collections;

public class slideButton : MonoBehaviour {

    GameObject score;
    GameObject speaker;

    public void Continue()
    {
        var slide = GameObject.Find("Slide");
        slide.SetActive(false);

        score.SetActive(true);
        speaker.SetActive(true);

        Time.timeScale = 1;
        Destroy(this.gameObject);
    }

    void Start()
    {
        score = GameObject.Find("/Main Camera/Score Text");
        speaker = GameObject.Find("/Canvas/Speaker");

        score.SetActive(false);
        speaker.SetActive(false);
    }
}
