using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI roundText;
    private string totalRounds;
    private string currentRound;
    
    // Start is called before the first frame update
    void Start()
    {
        totalRounds = LevelStatus.TotalRound.ToString();
        roundText.text = "Round 1/"+totalRounds;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentRound = LevelStatus.Round.ToString();
        roundText.text = "Round " + currentRound + "/" +totalRounds;
    }
}
