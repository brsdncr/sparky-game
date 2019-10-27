using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField] AudioClip cubeDeathAudio;
    [SerializeField] AudioClip playerDeathAudio;
    AudioSource backGroundMusic;

    private void Awake()
    {
        int numberOfAudioManager = FindObjectsOfType<AudioManager>().Length;
        if(numberOfAudioManager > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            backGroundMusic = GetComponent<AudioSource>();
            backGroundMusic.Play();
        }
    }

    private void Start()
    {
        //AudioSource.PlayClipAtPoint(backGroundMusic, new Vector3(0f, 0f, 0f));
        
    }

    public void PlayCubeDeath()
    {
        AudioSource.PlayClipAtPoint(cubeDeathAudio, new Vector3(0f, 0f, 0f));
    }

    public void PlayPlayerDeath()
    {
        AudioSource.PlayClipAtPoint(playerDeathAudio, new Vector3(0f, 0f, 0f));
    }
}
