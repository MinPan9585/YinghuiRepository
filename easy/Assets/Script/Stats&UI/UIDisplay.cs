using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Stats Display")]
    public TextMeshProUGUI roundText;
    public Image _livesFill;
    public Text livesText;
    public Text moneyText;

    private void Start()
    {
        GameEvents.Instance.OnUpdateDisplay += UpdateDisplay;
        UpdateDisplay();
    }
    private void OnDisable()
    {
        GameEvents.Instance.OnUpdateDisplay -= UpdateDisplay;
    }
    public void UpdateDisplay()
    {
        roundText.text = "Round " + LevelStatus.Round.ToString() + " / " + LevelStatus.TotalRound.ToString();
        _livesFill.fillAmount = (float)LevelStatus.Lives / 20f;
        livesText.text = LevelStatus.Lives.ToString() + "/20";
        moneyText.text = "$ " + LevelStatus.Money.ToString();
        
        //Debug.Log("UI display updatede. " + LevelStatus.Round + "  " + LevelStatus.Money);
    }
}
