using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Slide_S : MonoBehaviour {

    //public SpriteRenderer slide;
    public Sprite[] slideList;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = slideList[GameManager_S.wordID];
    }
}
