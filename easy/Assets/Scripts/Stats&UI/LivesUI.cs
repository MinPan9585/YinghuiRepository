using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    public Image _fill;

    public Text livesText;

    void Update()
    {
        _fill.fillAmount = (float)LevelStatus.Lives / 20f;

        livesText.text = PlayerStats.Lives.ToString() + "/20";
    }
}
