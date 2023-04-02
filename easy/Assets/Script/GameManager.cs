using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameEnded = false;
    public GameObject gameOverUI;
    public GameObject levelWinUI;

    public GameObject[] stuffToHide;

    public float transitionDuration = 1f; // The duration of the transition in seconds

    private void Start()
    {
        Time.timeScale = 1f;
        gameEnded = false;
        gameOverUI.SetActive(false);
        levelWinUI.SetActive(false);
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
        for (int i = 0; i < stuffToHide.Length; i++)
        {
            stuffToHide[i].SetActive(false);
        }

        StartCoroutine(TransitionTimeScale());
        StartCoroutine(DramaticLost());

        IEnumerator DramaticLost()
        {
            yield return new WaitForSeconds(1f);
            gameEnded = true;
            gameOverUI.SetActive(true);
        }
    }

    public void WinLevel()
    {
        for (int i = 0; i < stuffToHide.Length; i++)
        {
            stuffToHide[i].SetActive(false);
        }

        StartCoroutine(DramaticWin());
        
        IEnumerator DramaticWin()
        {
            yield return new WaitForSeconds(1f);
            gameEnded = true;
            levelWinUI.SetActive(true);
            //PlayerPrefs.SetInt("levelReached", 2);
            Debug.Log("Won!!!");
        }

    }

    

    IEnumerator TransitionTimeScale()
    {
        float elapsedTime = 0;
        float initialTimeScale = Time.timeScale; // Get the initial time scale

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            Time.timeScale = Mathf.Lerp(initialTimeScale, 0.1f, t); // Use Lerp to gradually change the time scale
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Time.timeScale = 0.1f; // Set the final time scale to 0.1
    }
}
