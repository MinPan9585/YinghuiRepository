using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.AI;

public class TurretShoot : MonoBehaviour
{

    [SerializeField] private Transform target;

    [Header("General")]
    public float range = 15f;
    public Animator myAnim;

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
    public float animationGap = 0.4f;

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
        // InvokeRepeating(nameof(GetEnemyTarget), 0f, coolDown);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            if (enemiesInRange.Count == 0)
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

            if (enemy.isdead)
            { continue; }
            else if (Physics.Raycast(transform.position,
                enemy.transform.position + new Vector3(0, 0.5f, 0) - transform.position,
                range, layerToBlock))
            { continue; }
            else if (Vector3.Distance(transform.position, enemy.transform.position) > range + 0.5f)
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

        //if (enemiesInRange.Count == 0)
        //{
        //    CancelInvoke(nameof(GetEnemyTarget));
        //}
        if(enemiesInRange.Count != 0)
        {
            target = enemiesInRange[0].transform;
            float minDistance = enemiesInRange[0].remainingDistance;
            foreach (EnemyBase enemy in enemiesInRange)
            {
                if (minDistance > enemy.remainingDistance)
                {
                    target = enemy.transform;
                }
            }
        }

        if (target != null)
        {
            StartRotating();//Shoot is called in rotate
        }
    }

    private void StartRotating()
    {
        //useRotate = true;
        if (target != null)
        {
            if (LookCorotine != null)
            {
                StopCoroutine(LookCorotine);
            }
            LookCorotine = StartCoroutine(LookAt());
        }
    }
    private IEnumerator LookAt()
    {
        
        float time = 0;
        while (time < 1)
        {
            if (target == null || !target.gameObject.activeSelf)
            {
                GetEnemyTarget();
                //Debug.Log("Attack another enemy");
            }
            else
            {
                Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);
                partToRotate.rotation = Quaternion.Slerp(partToRotate.transform.rotation, lookRotation, time);
                time += Time.deltaTime * turnSpeed;
            }
            yield return null;
        }

        if (target == null ||!target.gameObject.activeSelf)
        {
            GetEnemyTarget();
            //Debug.Log("Attack another enemy");
        }
    }
    void Shoot()
    {
        //Debug.Log(gameObject.name + ": Start Animation here");
        if (myAnim)
            myAnim.SetTrigger("Attack");
        GameObject projectile = PoolManager.Instance.SpawnFromSubPool(bulletPrefab.name.ToString(), transform);
        projectile.GetComponent<ISetTarget>().SetTarget(target);
        projectile.transform.SetParent(GameObject.Find("PooledPrefabs").transform, true);
        projectile.transform.SetPositionAndRotation(firePoint.position, partToRotate.rotation);
    }

    private float fireCountdown = 0f;
    private void FixedUpdate()
    {
        fireCountdown += Time.deltaTime;

        if (fireCountdown >= coolDown && target != null && target.gameObject.activeSelf)
        {
            fireCountdown = 0;
            Shoot();
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        if (target != null)
        {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(firePoint.transform.position, target.position);
        }
    }
}
