using UnityEngine;
using System.Collections;

public class menuScore : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GetComponent<TextMesh>().text = PlayerPrefs.GetInt("highScore").ToString();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
