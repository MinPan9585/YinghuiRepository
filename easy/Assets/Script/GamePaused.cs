using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GamePaused : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void Retry()
    {
        //SceneManager.LoadScene();
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        //SceneManager.LoadScene();
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void Resume()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
