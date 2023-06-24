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
    private bool lastRoundOut = false;

    [Header("Enemy")]
    public List<EnemyBase> enemyList;

    private void Awake()
    {


        foreach (RoundSpawner go in roundSpawners)
        {
            if (totalRoundNum < go.roundList.rounds.Count)
            {
                totalRoundNum = go.roundList.rounds.Count;
            }
        }
        LevelStatus.TotalRound = totalRoundNum;
        LevelStatus.Round = 0;
    }
    private void Start()
    {
        //GameEvents.Instance.UpdateDisplay();  //This line somehow doesn't work
        GameEvents.Instance.OnSpawnRound += TrySpawnARound;
        GameEvents.Instance.OnSwitchPath += RecalculateAllEnemiesPath;
        InvokeRepeating(nameof(CalculateAllEnemiesDistance), 1f, 0.5f);
    }
    private void OnDisable()
    {
        GameEvents.Instance.OnSpawnRound -= TrySpawnARound;
        GameEvents.Instance.OnSwitchPath -= RecalculateAllEnemiesPath;
        StopAllCoroutines();
        CancelInvoke();
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
                //go.poolManager.TidyUpAllSubPool();
                go.OnClickStartARound();
            }
            wavesAllOut = false;
            LevelStatus.Round++;
            GameEvents.Instance.UpdateDisplay();

            //Last round
            if(LevelStatus.Round == LevelStatus.TotalRound)
            {
                InvokeRepeating(nameof(CheckEnd), 10f, 2f);
            }
        }
    }
    void CheckEnd()
    {
        if (!lastRoundOut)
        {
            wavesAllOut = true;
            foreach (RoundSpawner go in roundSpawners)
            {
                if (!go.waveAllOut)
                {
                    wavesAllOut = false;
                    break;
                }
            }

            if(wavesAllOut)
                lastRoundOut = true;
        }
        else
        {
            enemyList = LevelStatus.EnemyBaseList;
            if(enemyList.Count == 0)
            {
                Debug.LogWarning("Win!!!");
                CancelInvoke();
            }
        }
        
    }

    public void CalculateAllEnemiesDistance()
    {
        enemyList = LevelStatus.EnemyBaseList;
        if(enemyList.Count > 0)
        {
            foreach (EnemyBase enemy in enemyList)
            {
                enemy.remainingDistance = enemy.GetPathRemainingDistance(true);
            }
        }
    }

    public void RecalculateAllEnemiesPath(int id)
    {
        enemyList = LevelStatus.EnemyBaseList;
        if (enemyList.Count > 0)
        {
            StartCoroutine(Recalculate());
        }
    }
    IEnumerator Recalculate()
    {
        yield return new WaitForSeconds(1);
        foreach (EnemyBase enemy in enemyList)
        {
            enemy.RecalculatePath();
        }
    }

}
