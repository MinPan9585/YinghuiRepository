using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Projectile : MonoBehaviour, IPooledObject, ISetTarget
{
    [SerializeField]private Transform target;
    [Header("Base parameter")]
    public float speed = 70f;
    public int damage = 50;
    private int actualDamage;
    public string enemyTag = "Enemy";
    [Header("Explosion")]
    public float explosionRadius;
    public float xRadius = 0.5f;
    [Header("Visual")]
    public GameObject hitEnemyFX;
    public GameObject flyFX;
    [Header("SFX")]
    public string shootSFX = "Buzzer";
    public string hitSFX = "Buzzer";


    [Header("Test parameters, ignore")]
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Vector3 deadPos;
    [SerializeField] private Vector3 targetPos;

    [SerializeField] bool purpose = true;
    #region Pool
    //Must have, even left blank. Also, put everything in Start() function here
    public void OnObjectSpawn()
    { 
        AudioManager.Instance.PlaySFX(shootSFX);
    }
    //Must have, even left blank.
    public void OnObjectDespawn()
    {
    }
    private void OnDisable()
    {
    }
    #endregion
    private void Start()
    {
        GameObject go = PoolManager.Instance.SpawnFromSubPool(flyFX.name.ToString(), transform);
        go.transform.SetParent(transform, false);
        go.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
        transform.LookAt(target);
        purpose = true;
    }
    void Update()
    {
        if (purpose)
        {
            if (target == null || target.GetComponent<EnemyBase>().isdead )
            {
                targetPos = deadPos;

                //Debug.Log("I have no purpose");
                purpose = false;
            }
            else
            {
                targetPos = target.position;
                deadPos = target.position;
            }
        }
        

        //position
        Vector3 dir = targetPos - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        //rotation
        transform.LookAt(targetPos);
        //hit enemy
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
    }

    private Transform UpdateTarget()
    {
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            if (!enemy.GetComponent<EnemyBase>().isdead)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }

        if (nearestEnemy != null)
        {
            return nearestEnemy.transform;
        }
        else
        {
            return null;
        }
    }

    void HitTarget()
    {
        SpawnFX();

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target, 1);
        }

        GetComponent<PooledObjectAttachment>().PutBackToPool();

    }

    private void SpawnFX()
    {
        AudioManager.Instance.PlaySFX(hitSFX);
        GameObject go = PoolManager.Instance.SpawnFromSubPool(hitEnemyFX.name.ToString(), transform);//This line needed for pooling
        go.transform.SetParent(GameObject.Find("PooledPrefabs").transform, true);
        go.transform.position = transform.position;
        //go.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(enemyTag))
            {
                float damageRatio = 1 - Vector3.Distance(collider.transform.position, transform.position) / (explosionRadius + xRadius);
                //Debug.Log("The damage ratio is: " + damageRatio);
                Damage(collider.transform, damageRatio);
            }
        }
    }

    void Damage(Transform enemy, float damageRatio)
    {
        //Debug.Log("Test phrase");

        ITakeDamage takeDamage = enemy.GetComponent<ITakeDamage>();
        if (takeDamage != null)
        {
            float floatDamage = damage * damageRatio;
            //Debug.Log("The damage taken by AOE Missile is: " + (int)floatDamage);
            actualDamage = (int)floatDamage;
            takeDamage.TakeDamage(actualDamage);
        }
        else
        {
            Debug.LogWarning("No component on the enemy found, check if it's the right script");
        }

        //Destroy(enemy.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    
}
