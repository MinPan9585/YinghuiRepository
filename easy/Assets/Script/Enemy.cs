using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

	public float speed = 10f;

	public float startHealth = 100f;
	private float health;

	public int value = 50;

	private Transform target;
	private int wavepointIndex = 0;

	[Header("Unity Stuff")]
	public Image healthBar;

	private bool isdead = false;
	private Waypoints0508 wp0508A;
	private Waypoints0508 wp0508B;

	void Start()
	{
		wp0508A = GameObject.Find("WayPoints1").GetComponent<Waypoints0508>();
		wp0508B = GameObject.Find("WayPoints2").GetComponent<Waypoints0508>();
		target = wp0508A.points[0];
		health = startHealth;
	}

	public void TakeDamage(int amount)
    {
		health -= amount;

		healthBar.fillAmount = health / startHealth;

		if(health <= 0 && !isdead )
        {
            isdead = true;
            Die();
        }
    }

	void Die()
    {
		PlayerStats.Money += value;
		WaveSpawner.EnemiesAlive--;
		Debug.Log(this + " died!");
		Destroy(gameObject);
    }

	void Update()
	{

        Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, target.position) <= 0.4f)
		{
			GetNextWaypoint();
		}
	}

	void GetNextWaypoint()
	{
		if (wavepointIndex >= wp0508A.points.Length - 1)
		{
			EndPath();
			
			return;
		}

		wavepointIndex++;
		target = wp0508A.points[wavepointIndex];
	}

	void EndPath()
    {
		PlayerStats.Lives--;
		WaveSpawner.EnemiesAlive--;
		Destroy(gameObject);
	}
}
