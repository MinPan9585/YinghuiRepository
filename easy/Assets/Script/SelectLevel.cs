using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    public Button[] levelButtons;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
    }
    public void SelectLevelNumber(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void Menu(string menuName)
    {
        SceneManager.LoadScene(menuName);
    }
}
