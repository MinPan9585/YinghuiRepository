using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMaster : MonoBehaviour
{
    public GameObject playButton;
    public GameObject inGameMenu;
    // public GameObject UpgradeMaster;

    public void OnMenuOpen()
    {
        // UpgradeMaster.SetActive(false);
        Time.timeScale = 0;
        playButton.SetActive(true);
        inGameMenu.SetActive(true);
    }
    
    public void OnPause()
    {
        Time.timeScale = 0;
        playButton.SetActive(true);
    }
    
    public void OnResume()
    {
        // UpgradeMaster.SetActive(true);
        Time.timeScale = 1f;
        playButton.SetActive(false);
        inGameMenu.SetActive(false);
    }

    public void OnRestart()
    {
        // UpgradeMaster.SetActive(true);
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
        Time.timeScale = 1f;
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
