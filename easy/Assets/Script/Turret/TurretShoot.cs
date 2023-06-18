using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.AI;

public class TurretShoot: MonoBehaviour
{

	[SerializeField] private Transform target;
	
	[Header("General")]
	public float range = 15f;

	[Header("Use Bullets (default)")]
	public GameObject bulletPrefab;
	public float coolDown = 1f;
    public float turnSpeed = 1f;

    [Header("Unity Setup Fields")]
	public Transform partToRotate;
	public Transform firePoint;
    public string enemyTag = "Enemy";
    public LayerMask enemyLayer;
    public LayerMask layerToBlock;

	//rotate
	private Coroutine LookCorotine;
    //find enemy closest to target
    private SphereCollider rangeCollider;
	[SerializeField] private List<EnemyBase> enemiesInRange;
	[SerializeField] private List<EnemyBase> enemySelction;
	[SerializeField] private List<float> higherSpeed;

    //private bool useRotate;

    void Start()
	{
		rangeCollider = GetComponent<SphereCollider>();
		rangeCollider.radius = range;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            if(enemiesInRange.Count == 0)
            {
                InvokeRepeating(nameof(GetEnemyTarget), 0f, coolDown);
            }
		}
    }

    void GetEnemyQualified()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, enemyLayer);
        foreach (Collider col in hitColliders)
        {
            EnemyBase enemy = col.GetComponent<EnemyBase>();
            
            if(enemy.isdead)
            { continue; }
            else if( Physics.Raycast(transform.position,
                enemy.transform.position + new Vector3(0, 0.5f, 0) - transform.position,
                range, layerToBlock))
            { continue; }
            else if(Vector3.Distance(transform.position, enemy.transform.position) > range + 0.5f) 
            { continue; }
            
            enemiesInRange.Add(enemy);
        }
    }
    private void GetEnemyTarget()
	{
        target = null;
        enemiesInRange.Clear();
        enemySelction.Clear();
        higherSpeed.Clear();
        
        GetEnemyQualified();

        if (enemiesInRange.Count == 0)
        {
            CancelInvoke(nameof(GetEnemyTarget));
        }
        else
		{
            target = enemiesInRange[0].transform;
            float minDistance = enemiesInRange[0].remainingDistance;
            foreach (EnemyBase enemy in enemiesInRange)
            {
                if(minDistance > enemy.remainingDistance)
                {
                    target = enemy.transform;
                }
            }
        }

		if(target != null)
		{
            StartRotating(target);//Shoot is called in rotate
        }
	}

	private void StartRotating(Transform enemyTarget)
	{
        //useRotate = true;
		if (enemyTarget != null)
		{
            if (LookCorotine != null)
            {
                StopCoroutine(LookCorotine);
            }
            LookCorotine = StartCoroutine(LookAt(enemyTarget));
        }
	}
	private IEnumerator LookAt(Transform enemyTarget)
	{
		Quaternion lookRotation = Quaternion.LookRotation(enemyTarget.position - transform.position);
		float time = 0;
		while (time < 1)
		{
			partToRotate.rotation = Quaternion.Slerp(partToRotate.transform.rotation, lookRotation, time);
			time += Time.deltaTime * turnSpeed;
			yield return null;
		}
        Shoot(enemyTarget);
        //useRotate = false;
	}
    void Shoot(Transform enemyTarget)
    {
        GameObject projectile = PoolManager.Instance.SpawnFromSubPool(bulletPrefab.name.ToString(), transform);
        projectile.GetComponent<ISetTarget>().SetTarget(enemyTarget);
        projectile.transform.SetParent(GameObject.Find("PooledPrefabs").transform, true);
        projectile.transform.SetPositionAndRotation(firePoint.position, partToRotate.rotation);
    }

    void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);

        if(target != null)
        {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(firePoint.transform.position, target.position);
        }
    }
}
