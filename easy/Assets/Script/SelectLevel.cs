using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{

    public void SelectLevelNumber(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void Menu(string menuName)
    {
        SceneManager.LoadScene(menuName);
    }
}
