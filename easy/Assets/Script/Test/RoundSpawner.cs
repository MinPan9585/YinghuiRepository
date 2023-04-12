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

    public int roundNum = 0;
    public float intervalAdded = 1f;
    public bool waveAllOut;
    public bool roundFinished = false;

    public PoolManager poolManager;

    public Wave theWave= null;
    private void Start()
    {
        roundFinished = false;
        waveAllOut = true;
        roundNum = 0;
        poolManager = PoolManager.Instance;
        Debug.Log("There are " + roundList.rounds.Count + " rounds in this level");
    }
    public void OnClickStartARound()
    {
        if(waveAllOut == true)
        {
            waveAllOut = false;
            StartCoroutine(SpawnARound());
        }
        else
        {
            Debug.Log("Current round still running, can't release next round");
        }
    }
    IEnumerator SpawnARound()
    {
        Round round = roundList.rounds[roundNum];

        foreach (Wave wave in round.waves)
        {
            theWave = wave;
            Debug.Log(theWave.enemyPrefab);
            StartCoroutine(SpawnAWave());

            yield return new WaitForSeconds(wave.interval + intervalAdded);

        }

        roundNum++;
        
        if(roundNum >= roundList.rounds.Count)
        {
            Debug.Log("All rounds are released");
        }
        waveAllOut = true;
    }

    IEnumerator SpawnAWave()
    {
        for (int i = 0; i < theWave.count; i++)
        {            
            SpawnAEnemy(theWave.enemyPrefab);
            yield return new WaitForSeconds(1f / theWave.rate);
        }
    }
    void SpawnAEnemy(GameObject prefab)
    {
        Debug.Log(theWave.enemyPrefab.name);
        GameObject obj = poolManager.SpawnFromSubPool(prefab.name.ToString(), transform);//This line needed for pooling
        obj.transform.position = transform.position + Random.onUnitSphere * 2;
    }
    public void OnButtonClear()
    {
        poolManager.ClearAllSubPool();//This line needed for pooling
    }
}
