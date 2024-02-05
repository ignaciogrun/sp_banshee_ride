using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip[] audios;

    private AudioSource Source;
    private string currentScene;

    private int k;

    public bool EndScene = false;

    // Start is called before the first frame update
    void Awake()
    {
        Source = this.GetComponent<AudioSource>();
        currentScene = SceneManager.GetActiveScene().name;
    }

    void Start()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        if (currentScene == "Main Menu")
        {
            Source.Stop();
            Source.clip = audios[0];
            Source.Play();
        }
        if (currentScene == "Level 1")
        {
            Source.Stop();
            Source.clip = audios[2];
            Source.Play();
        }
    }

    
    // Update is called once per frame
    void Update()
    {

        if (currentScene == "Main Menu")
        {
            if (!Source.isPlaying)
            {
                if (Source.clip != audios[4])
                    Source.clip = audios[1];
                Source.Play();
            }
        }
        if (currentScene == "Level 1" && EndScene == false)
        {
            if (!Source.isPlaying)
            {
                if (Source.clip != audios[4])
                    Source.clip = audios[3];
                Source.Play();
            }
        }
        if(currentScene == "Level 1" && EndScene == true)
        {
            Source.Stop();
            Source.clip = audios[4];
            Source.Play();
        }
    }
}
