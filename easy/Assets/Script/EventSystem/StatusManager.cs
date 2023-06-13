using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    [Header("Round Spawner")]
    public List<RoundSpawner> roundSpawners;
    private int totalRoundNum = 0;
    private bool wavesAllOut = true;

    private void Start()
    {
        GameEvents.Instance.OnSpawnRound += TrySpawnARound;

        foreach (RoundSpawner go in roundSpawners)
        {
            if(totalRoundNum < go.roundList.rounds.Count)
            {
                totalRoundNum = go.roundList.rounds.Count;
            }
        }
        LevelStatus.TotalRound = totalRoundNum;
        GameEvents.Instance.UpdateDisplay();
    }
    private void OnDisable()
    {
        GameEvents.Instance.OnSpawnRound -= TrySpawnARound;
    }

    public void TrySpawnARound()
    {
        wavesAllOut = true;
        foreach (RoundSpawner go in roundSpawners)
        {
            if (!go.waveAllOut)
            {
                wavesAllOut = false;
                Debug.Log("Current round still running");
                break;
            }
        }

        if (wavesAllOut && LevelStatus.Round < LevelStatus.TotalRound)
        {
            foreach (RoundSpawner go in roundSpawners)
            {
                go.OnClickStartARound();
            }
            wavesAllOut = false;
            LevelStatus.Round++;
            GameEvents.Instance.UpdateDisplay();
        }
    }

    
}
