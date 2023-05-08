using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

	public float speed = 10f;

	public float startHealth = 100f;
	private float health;

	public int value = 50;

	private Transform target;
	public int wavepointIndex = 0;

	[Header("Unity Stuff")]
	public Image healthBar;

	private bool isdead = false;
	public Waypoints0508 wp0508A;
	public Waypoints0508 wp0508B;
	public Waypoints0508 currentWay;
	private SwitchPath01 switchPathA;

	void Start()
	{
		wp0508A = GameObject.Find("WayPoints1").GetComponent<Waypoints0508>();
		wp0508B = GameObject.Find("WayPoints2").GetComponent<Waypoints0508>();
		switchPathA = Object.FindObjectOfType<SwitchPath01>();
        if (switchPathA.isLeft)
        {
			currentWay = wp0508A;
        }
        else
        {
			currentWay = wp0508B;
		}
		
		target = currentWay.points[0];
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
		if (wavepointIndex >= currentWay.points.Length - 1)
		{
			EndPath();
			
			return;
		}

		wavepointIndex++;
		target = currentWay.points[wavepointIndex];
	}

	void EndPath()
    {
		PlayerStats.Lives--;
		WaveSpawner.EnemiesAlive--;
		Destroy(gameObject);
	}
}
