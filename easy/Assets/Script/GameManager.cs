using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static bool gameEnded = false;
    public GameObject gameOverUI;
    public GameObject levelWinUI;

    private void Start()
    {
        gameEnded = false;
    }

    void Update()
    {
        if (gameEnded)
            return;

        if(PlayerStats.Lives <= 0)
        {
            EndGame();
        }


    }

    void EndGame()
    {
        gameEnded = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        levelWinUI.SetActive(true);
        //PlayerPrefs.SetInt("levelReached", 2);
    }
}
