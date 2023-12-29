using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public AudioClip IntroMusic;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (IntroMusic != null)
        {
            audioSource.clip = IntroMusic;
            audioSource.Play();
        }
    }

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnGameOverButtonClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}
