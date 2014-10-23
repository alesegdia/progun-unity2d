using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

    AudioSource bgmusic;
    bool showing = true;
	// Use this for initialization

	void Start () {
        showing = true;
        DontDestroyOnLoad(transform.gameObject);
        bgmusic = GetComponent<AudioSource>();
        bgmusic.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (showing && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            showing = false;
            Application.LoadLevel("Gameplay");
        }
	}
}
