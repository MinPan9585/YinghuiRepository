using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelWon : MonoBehaviour
{
    public void Retry()
    {
        //SceneManager.LoadScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        //SceneManager.LoadScene();
        SceneManager.LoadScene(0);
    }
    public void Continue()
    {
        SceneManager.LoadScene(1);
    }
}
