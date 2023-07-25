using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Round
    {
        public List<Wave> waves;
    }
    [System.Serializable]
    public class RoundList
    {
        public List<Round> rounds;
    }

    public RoundList roundList = new RoundList();

    [Header("Test parameter, ignore")]
    public int roundNum = 0;
    public float intervalAdded = 1f;
    public bool waveAllOut;
    public bool roundFinished = false;

    public Transform startPoint;
    //This variable is for pooling
    public PoolManager poolManager;

    public Wave theWave= null;

    public StatusManager statusManager;
    private void Start()
    {
        startPoint = GameObject.Find("Start").transform;
        roundFinished = false;
        waveAllOut = true;
        roundNum = 0;
        poolManager = PoolManager.Instance;
        //Debug.Log("There are " + roundList.rounds.Count + " rounds in this level");
    }
    public void SetStatusManager(StatusManager statusManager)
    {
        this.statusManager = statusManager;
    }
    public void OnClickStartARound()
    {
        if(waveAllOut == true)
        {
            waveAllOut = false;
            StartCoroutine(SpawnARound());
        }
        
    }
    IEnumerator SpawnARound()
    {
        if(roundNum < roundList.rounds.Count)
        {
            Round round = roundList.rounds[roundNum];

            foreach (Wave wave in round.waves)
            {
                theWave = wave;
                Debug.Log("RoundSpawner: A new wave is realsed with: "  + theWave.enemyPrefab);
                StartCoroutine(SpawnAWave(theWave));

                yield return new WaitForSeconds(wave.interval + intervalAdded);

            }

            roundNum++;
        }
        else
        {
            Debug.Log("All rounds are released");
        }

        //Set the round state when all waves are out
        waveAllOut = true;
        statusManager.PreviewEnemy();
    }

    IEnumerator SpawnAWave(Wave theWave)
    {
        for (int i = 0; i < theWave.count; i++)
        {            
            SpawnAEnemy(theWave.enemyPrefab);
            yield return new WaitForSeconds(1f / theWave.rate);
        }
    }
    void SpawnAEnemy(GameObject prefab)
    {
        //Debug.Log(theWave.enemyPrefab.name);
        GameObject obj = poolManager.SpawnFromSubPool(prefab.name.ToString(), startPoint.transform);//This line needed for pooling
        //obj.transform.SetParent(GameObject.Find("PooledPrefabs").transform, true);//Remeber to set this line in the end
        obj.transform.SetParent(startPoint.transform, true);

        if (obj.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            LevelStatus.EnemyBaseList.Add(enemy);
        }

    }

    public List<Wave> GetNextRound()
    {
        Round round = new Round();
        if(roundNum < roundList.rounds.Count)
        {
            round = roundList.rounds[roundNum];
        }
        return round.waves;
    }
    public void OnButtonClear()
    {
        poolManager.ClearAllSubPool();//This line needed for pooling
    }
}