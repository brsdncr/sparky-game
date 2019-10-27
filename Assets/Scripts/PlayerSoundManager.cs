using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour {

    AudioSource audioSource;
    bool soundActive = false;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
	}

    private void Update()
    {
        CheckIfSoundShouldPlay();
    }

    private void CheckIfSoundShouldPlay()
    {
        //if mouse is in the screen
        if(transform.position.x > -3 && transform.position.x < 3 && transform.position.y < 7 && transform.position.y > -5)
        {
            if (!soundActive)
            {
                soundActive = true;
                audioSource.Play();
            }
        }
        else
        {
            soundActive = false;
            audioSource.Stop();
        }
    }
}
