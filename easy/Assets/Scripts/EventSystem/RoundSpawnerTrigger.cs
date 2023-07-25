using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundSpawnerTrigger : MonoBehaviour
{
    [Header("Testing field, ignore")]
    [SerializeField] private Button roundSpawnerButton;

    private void Start()
    {
        roundSpawnerButton = GameObject.Find("NextRoundButton").GetComponent<Button>();
        roundSpawnerButton.onClick.AddListener(SwitchPath);
    }

    private void SwitchPath()
    {
        //Debug.Log("Button is pressed, the path should be switched");
        GameEvents.Instance.SpawnRound();
    }
}

