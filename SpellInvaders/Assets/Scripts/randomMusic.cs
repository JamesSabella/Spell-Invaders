using UnityEngine;
using System.Collections;

public class randomMusic : MonoBehaviour {

    public AudioClip[] songList = new AudioClip[5];


    // Use this for initialization
    void Start () {

        if (LoadOnClick.isShuffled == false)
        {
            reshuffle(songList);
            LoadOnClick.isShuffled = true;
        }


        if (LoadOnClick.i == 5)
            LoadOnClick.i = 0;
        this.GetComponent<AudioSource>().clip = songList[LoadOnClick.i];
        this.GetComponent<AudioSource>().Play();
        Debug.Log(LoadOnClick.i);
        LoadOnClick.i++;
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void reshuffle(AudioClip[] songList)
    {
        //Knuth shuffle algorithm
        for (int t = 0; t < songList.Length; t++)
        {
            AudioClip tmp = songList[t];
            int r = Random.Range(t, songList.Length);
            songList[t] = songList[r];
            songList[r] = tmp;
        }
    }

}
