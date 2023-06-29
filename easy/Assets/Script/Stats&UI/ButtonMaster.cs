using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMaster : MonoBehaviour
{
    
    public GameObject playButton;
    public GameObject inGameMenu;
    public GameObject previewUI;

    // public GameObject SFXListener;
    // public GameObject UpgradeMaster;

    public void OnMenuOpen()
    {
        GameEvents.Instance.MenuDisplay(true);
        // UpgradeMaster.SetActive(false);
        Time.timeScale = 0;
        playButton.SetActive(true);
        inGameMenu.SetActive(true);
        previewUI.SetActive(false);
        // SFXListener.SetActive(false);
    }
    
    public void OnPause()
    {
        GameEvents.Instance.MenuDisplay(true);
        Time.timeScale = 0;
        playButton.SetActive(true);
        // SFXListener.SetActive(false);
    }
    
    public void OnResume()
    {
        GameEvents.Instance.MenuDisplay(false);
        // UpgradeMaster.SetActive(true);
        Time.timeScale = 1f;
        playButton.SetActive(false);
        inGameMenu.SetActive(false);
        previewUI.SetActive(true);
        // SFXListener.SetActive(true);
    }

    public void OnRestart()
    {
        // UpgradeMaster.SetActive(true);
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
        Time.timeScale = 1f;
        // SFXListener.SetActive(true);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
