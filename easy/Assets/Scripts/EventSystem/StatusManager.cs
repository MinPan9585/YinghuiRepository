using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    [Header("Round Spawner")]
    public List<RoundSpawner> roundSpawners;
    private int totalRoundNum = 0;
    [Header("Parameters,ignore")]
    [SerializeField] private bool wavesAllOut = true;
    private bool lastRoundOut = false;
    public List<Wave> previewWaves = new List<Wave>();

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
            go.SetStatusManager(this);
        }
        LevelStatus.TotalRound = totalRoundNum;
        LevelStatus.Round = 0;
        LevelStatus.Lives = 20;
        LevelStatus.Money = 400;
    }
    private void Start()
    {
        //GameEvents.Instance.UpdateDisplay();  //This line somehow doesn't work
        GameEvents.Instance.OnSpawnRound += TrySpawnARound;
        GameEvents.Instance.OnSwitchPath += RecalculateAllEnemiesPath;
        InvokeRepeating(nameof(CalculateAllEnemiesDistance), 1f, 0.5f);
        //PreviewEnemy();
        Invoke(nameof(PreviewEnemy), 0.1f);
        enemyList.Clear();
        Time.timeScale= 1f;
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
                return;
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
            GameEvents.Instance.RoundSpawned();
            //Last round
            if (LevelStatus.Round == LevelStatus.TotalRound)
            {
                InvokeRepeating(nameof(CheckEnd), 10f, 2f);
            }
        }
    }

    public void PreviewEnemy()
    {
        previewWaves = null;
        if (LevelStatus.Round < LevelStatus.TotalRound)
        {
            foreach (RoundSpawner go in roundSpawners)
            {
                previewWaves = go.GetNextRound();
                //foreach (Wave wave in previewWaves)
                //{
                //    Debug.Log("Enemy: " + wave.enemyPrefab + " Amount: " + wave.count);
                //}
                //Debug.Log("Preview Enemy");
                GameEvents.Instance.PreviewEnemy(previewWaves);
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

            if (wavesAllOut)
                lastRoundOut = true;
        }
        else
        {
            enemyList = LevelStatus.EnemyBaseList;
            if (enemyList.Count == 0 && !LevelStatus.Die)
            {
                GameEvents.Instance.WinTheGame();
                Debug.LogWarning("Win!!!");
                CancelInvoke();
            }
        }

    }

    public void CalculateAllEnemiesDistance()
    {
        enemyList = LevelStatus.EnemyBaseList;
        if (enemyList.Count > 0)
        {
            foreach (EnemyBase enemy in enemyList)
            {
                if(enemy != null)
                    enemy.remainingDistance = enemy.GetPathRemainingDistance(true);
                else
                    enemyList.Remove(enemy);
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
