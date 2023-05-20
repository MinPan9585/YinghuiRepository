using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyNav0519 : MonoBehaviour, IPooledObject
{

	public float speed = 10f;

	public float startHealth = 100f;
    [SerializeField] private float health;

	public int value = 50;

    [SerializeField] private Transform start;
    [SerializeField] private Transform target;
    public NavMeshAgent agent;


	[Header("Unity Stuff")]
	public Image healthBar;

    [SerializeField] private bool isdead = false;

    private void Awake()
    {
        //RestoreValues();
        start = GameObject.Find("Start").transform;
        agent.Warp(start.position);
        target = GameObject.Find("End").transform;
        Debug.Log(target.name);
        //agent.enabled = true;
        agent.SetDestination(target.position);
    }

    //using pool start
    //Must have, even left blank. Also, put everything in Start() function here
    public void OnObjectSpawn()
    {
        start = GameObject.Find("Start").transform;
        agent.Warp(start.position);
        //agent.enabled = true;
        RestoreValues();
        target = GameObject.Find("End").transform;
        Debug.Log(target.name);
        
        agent.SetDestination(target.position);
    }

    //Must have, now have some issues, just leave it blank.
    public void OnObjectDespawn() { }

    //specifically for restoring some values, like health
    public void RestoreValues()
    {
        //reset die only once bool
        isdead = false;

        //reset health
        health = startHealth;
        //healthBar.fillAmount = health / startHealth;

        //reset movement status (recommended for every pooled object)
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    private void OnDisable()
    {
        Debug.Log("Just got diabled");
    }
    //using pool end


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
		//WaveSpawner.EnemiesAlive--;
		Debug.Log(this + " died!");
        //Destroy(gameObject);
        GetComponent<PooledObjectAttachment>().PutBackToPool();
    }

    void Update()
	{
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.position) <= 0.4f)
            {
                EndPath();
            }
        }
        
    }


	void EndPath()
    {
        //agent.enabled = false;
        PlayerStats.Lives--;
		WaveSpawner.EnemiesAlive--;
        //Destroy(gameObject);
        
        GetComponent<PooledObjectAttachment>().PutBackToPool();
    }
}
