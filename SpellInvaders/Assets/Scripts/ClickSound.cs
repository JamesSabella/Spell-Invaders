using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class ClickSound : MonoBehaviour
{
    public AudioClip word;
    private Button button { get { return GetComponent<Button>(); } }
    public TextMesh speaker;
    public static List<AudioClip[]> sentenceList;

    // Use this for initialization
    void Start()
    {
        sentenceList = new List<AudioClip[]>()
    {
        new AudioClip[] {(AudioClip)Resources.Load("finished words/weather"), (AudioClip)Resources.Load("finished words/whether") },
        new AudioClip[] {(AudioClip)Resources.Load("finished words/theyre"), (AudioClip)Resources.Load("finished words/there"), (AudioClip)Resources.Load("finished words/their") },
        new AudioClip[] {(AudioClip)Resources.Load("finished words/affect"), (AudioClip)Resources.Load("finished words/effect") },
        new AudioClip[] {(AudioClip)Resources.Load("finished words/by"), (AudioClip)Resources.Load("finished words/bye"), (AudioClip)Resources.Load("finished words/buy") },
        new AudioClip[] {(AudioClip)Resources.Load("finished words/aloud"), (AudioClip)Resources.Load("finished words/allowed") },
        new AudioClip[] {(AudioClip)Resources.Load("finished words/flower"), (AudioClip)Resources.Load("finished words/flour") },
        new AudioClip[] {(AudioClip)Resources.Load("finished words/blue"), (AudioClip)Resources.Load("finished words/blew") },
        new AudioClip[] {(AudioClip)Resources.Load("finished words/bare"), (AudioClip)Resources.Load("finished words/bear") },
        new AudioClip[] {(AudioClip)Resources.Load("finished words/brake"), (AudioClip)Resources.Load("finished words/break") },
        new AudioClip[] {(AudioClip)Resources.Load("finished words/course"), (AudioClip)Resources.Load("finished words/coarse") },
        new AudioClip[] {(AudioClip)Resources.Load("finished words/write"), (AudioClip)Resources.Load("finished words/right") },
        new AudioClip[] {(AudioClip)Resources.Load("finished words/principal"), (AudioClip)Resources.Load("finished words/principle") }

    };
        gameObject.AddComponent<AudioSource>();
        GetComponent<AudioSource>().clip = word;
        GetComponent<AudioSource>().PlayOneShot(sentenceList[GameManager_S.wordID][GameManager_S.wordIDIndex]);
        button.onClick.AddListener(() => PlaySound());
    }

    void PlaySound()
    {
        GetComponent<AudioSource>().PlayOneShot(sentenceList[GameManager_S.wordID][GameManager_S.wordIDIndex]);
        Destroy(speaker);
    }
}