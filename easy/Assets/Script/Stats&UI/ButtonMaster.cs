using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMaster : MonoBehaviour
{

    public GameObject playButton;
    public GameObject pauseButton;
    public GameObject inGameMenu;
    public GameObject previewUI;

    public GameObject LoseUI;
    public GameObject WinUI;

    public List<ParticleSystem> victoryParticle;
    // public GameObject SFXListener;
    // public GameObject UpgradeMaster;
    public void Start()
    {
        GameEvents.Instance.OnDie += CallLoseUI;
        GameEvents.Instance.OnWin += CallWinUI;

        if (LoseUI != null)
            LoseUI.SetActive(false);
        if (WinUI != null)
            WinUI.SetActive(false);

        if (victoryParticle.Count > 0)
        {
            foreach (ParticleSystem particle in victoryParticle)
            {
                particle.Stop();
            }
        }
    }
    public void OnDisable()
    {
        GameEvents.Instance.OnDie -= CallLoseUI;
        GameEvents.Instance.OnWin -= CallWinUI;
        CancelInvoke();
    }

    public void CallLoseUI()
    {
        GameEvents.Instance.MenuDisplay(true);
        Time.timeScale = 0f;
        Debug.Log("LoseUI");
        if (LoseUI != null)
        {
            previewUI.SetActive(false);
            LoseUI.SetActive(true);
        }
    }
    public void CallWinUI()
    {
        GameEvents.Instance.MenuDisplay(true);
        Debug.Log("WinUI");
        if (WinUI != null)
        {
            WinUI.SetActive(true);
        }

        if (victoryParticle.Count > 0)
        {
            foreach (ParticleSystem particle in victoryParticle)
            {
                particle.Play();
            }
        }
        Invoke(nameof(FreezeTime), 5f);
    }
    void FreezeTime()
    {
        Time.timeScale = 0f;
    }
    public void OnNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            SceneManager.LoadScene(2);
        if (SceneManager.GetActiveScene().buildIndex == 2)
            SceneManager.LoadScene(2);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
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
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0;
            playButton.SetActive(true);
            pauseButton.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            playButton.SetActive(false);
            pauseButton.SetActive(true);
        }

        // playButton.SetActive(true);
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
