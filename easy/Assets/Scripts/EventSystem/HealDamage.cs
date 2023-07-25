using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealDamage : MonoBehaviour
{
    private Button healButton;
    private Button damageButton;

    public TMP_Text livesText;
    
    private void Awake()
    {
        healButton = GameObject.Find("HealButton").GetComponent<Button>();
        damageButton = GameObject.Find("DamageButton").GetComponent<Button>();

    }

    private void Start()
    {
        livesText = GameObject.Find("LivesDisplayText").GetComponent<TMP_Text>();

        healButton.onClick.AddListener(Heal);
        damageButton.onClick.AddListener(Damage);

        //Subscribe the event
        GameEvents.Instance.OnDie += EndThisLevel;
    }

    private void Heal()
    {
        LevelStatus.Lives += 20;
        livesText.text = LevelStatus.Lives.ToString();
    }

    private void Damage()
    {
        LevelStatus.Lives -= 20;
        if (LevelStatus.Lives > 0)
            livesText.text = LevelStatus.Lives.ToString();
        else
            livesText.text = "X _ X";
    }

    private void OnDisable()
    {
        //Unsubscribe the event
        GameEvents.Instance.OnDie -= EndThisLevel;
    }

    private void EndThisLevel()
    {
        Debug.Log("Try harder next time");
    }
}
