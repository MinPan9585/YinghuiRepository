using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    public GameObject enemyBook, ProductionGroup;
    public Button l1, l2, Tutorial, Exit;
    public bool CheckedTutorial;
    private void Start()
    {
        CheckedTutorial = false;
        l1.interactable = false;
        l2.interactable = false;
        enemyBook.SetActive(false);
        ProductionGroup.SetActive(false);
    }
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

    public void EnemyBookOpen()
    {
        CheckedTutorial = true;
        enemyBook.SetActive(true);
        l1.interactable = true;
        l2.interactable = true;
        Exit.gameObject.SetActive(false);
    }

    public void EnemyBookClose()
    {
        enemyBook.SetActive(false);
        Exit.gameObject.SetActive(true);
    }
    public void GroupOpen()
    {
        ProductionGroup.SetActive(true);
        Exit.gameObject.SetActive(false);
    }

    public void GroupClose()
    {
        ProductionGroup.SetActive(false);
        Exit.gameObject.SetActive(true);
    }
}
