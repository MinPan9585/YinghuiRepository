using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win_Lose_UI : MonoBehaviour
{
    public GameObject LoseUI;
    public GameObject WinUI;

    public void Start()
    {
        GameEvents.Instance.OnDie += CallLoseUI;
        GameEvents.Instance.OnWin += CallWinUI;
        
        if (LoseUI != null)
            LoseUI.SetActive(false);
        if(WinUI != null)
            WinUI.SetActive(false);
        
    }
    public void OnDisable()
    {
        GameEvents.Instance.OnDie -= CallLoseUI;
        GameEvents.Instance.OnWin -= CallWinUI;
    }

    public void CallLoseUI()
    {
        Time.timeScale = 0f;
        Debug.Log("LoseUI");
        if(LoseUI != null)
        {
            LoseUI.SetActive(true);
        }
    }
    public void CallWinUI()
    {
        Time.timeScale = 0f;
        Debug.Log("WinUI");
        if(WinUI != null)
        {
            WinUI.SetActive(true);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
