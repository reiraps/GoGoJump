using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gamemanager;
    public Text scoreText;
    public GameObject gameOverUI;
    public bool isGameOver = false;
    //private int score = 0;

    void Awake()
    {
        if (gamemanager == null)
        {
            gamemanager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (isGameOver && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            isGameOver = false;
        }
    }
    //public void AddScore(int newScore)
    //{
    //    if (!isGameOver)
    //    {
    //        score += newScore;
    //        scoreText.text = "Score : " + score;
    //    }
    //}

    public void OnPlayerDead()
    {
        isGameOver = true;

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }

    public void OnClearZoneReached()
    {
        isGameOver = true;

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }
}