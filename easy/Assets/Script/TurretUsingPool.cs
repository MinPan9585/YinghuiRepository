using UnityEngine;
using System.Collections;

public class TurretUsingPool : MonoBehaviour
{

	private Transform target;
	[Header("General")]
	public float range = 15f;

	[Header("Use Bullets (default)")]
	public GameObject bulletPrefab;
	public float fireRate = 1f;
	private float fireCountdown = 0f;

	[Header("Unity Setup Fields")]
	public string enemyTag = "Enemy";
	public Transform partToRotate;
	public float turnSpeed = 10f;	

	public Transform firePoint;

    void Start()
	{
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }

	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
		}
		else
		{
			target = null;
		}

	}

	void Update()
	{
		if (target == null)
        {
			return;
		}

		LockOnTarget();

        if (fireCountdown <= 0f)
        {
			if (!Physics.Raycast(transform.position, target.transform.position + new Vector3(0, 0.5f, 0) - transform.position,
						(range - 0.8f), ~3))
			{
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }
        fireCountdown -= Time.deltaTime;

    }
	void Shoot()
	{
        //GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject bulletGO = PoolManager.Instance.SpawnFromSubPool(bulletPrefab.name.ToString(), transform);//This line needed for pooling
        bulletGO.transform.SetParent(GameObject.Find("PooledPrefabs").transform, true);
		bulletGO.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
		

	    //Bullet bullet = bulletGO.GetComponent<Bullet>();
		//if (bullet != null)
		//	bullet.Seek(target);
	}


	void LockOnTarget()
    {
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
