using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Stats Display")]
    public TextMeshProUGUI roundText;
    // public Image _livesFill;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI moneyText;

    private bool isIntro = false;

    private void Start()
    {
        GameEvents.Instance.OnUpdateDisplay += UpdateDisplay;
        UpdateDisplay();
    }

    //private void Update()
    //{
    //    if (LevelStatus.Money >= 200 && !isIntro && SceneManager.GetActiveScene().name == "Level_1_3")
    //    {
    //        GameEvents.Instance.UpdateIntro("upgradeIntro");
    //        isIntro = true;
    //    }
    //}

    private void OnDisable()
    {
        GameEvents.Instance.OnUpdateDisplay -= UpdateDisplay;
    }
    public void UpdateDisplay()
    {
        roundText.text = LevelStatus.Round.ToString() + "/" + LevelStatus.TotalRound.ToString();
        // _livesFill.fillAmount = (float)LevelStatus.Lives / 20f;
        livesText.text = LevelStatus.Lives.ToString();
        moneyText.text = LevelStatus.Money.ToString();
        
        //Debug.Log("UI display updatede. " + LevelStatus.Round + "  " + LevelStatus.Money);
    }
}
