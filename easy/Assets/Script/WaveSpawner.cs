using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
//using UnityEditor.Experimental.GraphView;

public class WaveSpawner : MonoBehaviour
{
	public static int EnemiesAlive = 0;

	public Wave[] waves;

	public Transform enemyPrefab;

	public Transform spawnPoint;

	public float timeBetweenWaves = 3f;
	private float countdown = 2f;
	public Text waveCountdownText;

	public Text roundsText;
	private int waveIndex = 0;

	public GameManager gameManager;

    private void Awake()
    {
        EnemiesAlive= 0;
    }
    void Update()
	{
        if (EnemiesAlive > 0)
        {
			return;
        }

        if (waveIndex == waves.Length)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
			{
                gameManager.WinLevel();
                this.enabled = false;
            } 
        }

        if (countdown <= 0f && !GameManager.gameEnded)
		{
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
			return;
		}


		countdown -= Time.deltaTime;

		countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

		waveCountdownText.text = string.Format("{0:00.00}", countdown);

    }

	IEnumerator SpawnWave()
	{
		Wave wave = waves[waveIndex];
        waveIndex++;
        PlayerStats.Rounds = waveIndex;
        roundsText.text = waveIndex.ToString() + " / " + waves.Length + "Rounds";
        for (int i = 0; i < wave.count; i++)
		{
			SpawnEnemy(wave.enemyPrefab);
			yield return new WaitForSeconds(1f / wave.rate);
		}
	}

	void SpawnEnemy( GameObject enemy )
	{
		GameObject myEnemy = Instantiate(enemy, spawnPoint.position, Quaternion.identity);
		EnemiesAlive++;
        //myEnemy.AddComponent<AttachToClosestChild>();
        myEnemy.transform.parent = GameObject.Find("Waypoints").transform;

    }

}
