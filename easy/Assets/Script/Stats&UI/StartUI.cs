using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    public void Level1Start()
    {
        SceneManager.LoadScene(1);
    }

    public void Level2Start()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitInStart()
    {
        Application.Quit();
    }
}
