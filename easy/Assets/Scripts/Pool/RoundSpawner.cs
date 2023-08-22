using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public RoundList roundList;

    [Header("Test parameter, ignore")]
    public int roundNum = 0;
    public float intervalAdded = 1f;
    public bool waveAllOut;
    public bool roundFinished = false;

    public Transform startPoint;
    //This variable is for pooling
    public PoolManager poolManager;

    public Wave theWave = null;

    public StatusManager statusManager;

    private void Awake()
    {
        LoadRoundDataFromJson();
    }

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
        if (waveAllOut == true)
        {
            waveAllOut = false;
            StartCoroutine(SpawnARound());
        }

    }
    IEnumerator SpawnARound()
    {
        if (roundNum < roundList.rounds.Count)
        {
            Round round = roundList.rounds[roundNum];

            foreach (Wave wave in round.waves)
            {
                theWave = wave;
                Debug.Log("RoundSpawner: A new wave is realsed with: " + theWave.enemyPrefabName);
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
            SpawnAEnemy(theWave.enemyPrefabName);
            yield return new WaitForSeconds(1f / theWave.rate);
        }
    }

    private Dictionary<string, GameObject> prefabCache = new Dictionary<string, GameObject>();

    void SpawnAEnemy(string prefabName)
    {
        if (!prefabCache.ContainsKey(prefabName))
        {
            GameObject prefab = Resources.Load<GameObject>("pool/" + prefabName);
            if (prefab != null)
            {
                prefabCache[prefabName] = prefab;
            }
            else
            {
                Debug.LogError("Cannot find prefab: " + prefabName);
                return;
            }
        }

        GameObject obj = poolManager.SpawnFromSubPool(prefabName, startPoint.transform);
        obj.transform.SetParent(startPoint.transform, true);

        if (obj.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            LevelStatus.EnemyBaseList.Add(enemy);
        }
    }

    public List<Wave> GetNextRound()
    {
        Round round = new Round();
        if (roundNum < roundList.rounds.Count)
        {
            round = roundList.rounds[roundNum];
        }
        return round.waves;
    }
    public void OnButtonClear()
    {
        poolManager.ClearAllSubPool(); //This line needed for pooling
    }

    public void LoadRoundDataFromJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("EnemyRoundJson/" + SceneManager.GetActiveScene().name);
        if (jsonFile)
        {
            string jsonData = jsonFile.text;
            roundList = JsonUtility.FromJson<RoundList>(jsonData);
        }
        else
        {
            Debug.LogError("Cannot find round data JSON file!");
        }
    }
}
