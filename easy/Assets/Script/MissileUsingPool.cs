using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileUsingPool : MonoBehaviour, IPooledObject
{
    private Transform target;

    public float speed = 70f;

    public int damage = 50;
    private int actualDamage;

    public float explosionRadius;
    public float affRadius = 0.5f;
    public GameObject hitEnemyFX;

    public string enemyTag = "Enemy";
    [SerializeField] private GameObject[] enemies;

    //rotation variables
    private Vector3 rotFormal;
    private Vector3 rotGoal;
    private float rotTimeCount = 0.0f;

    //This variable is for pooling, yea this pooled object is pooling other pooled objects
    public PoolManager poolManager;

    //using pool start
    //Must have, even left blank. Also, put everything in Start() function here
    public void OnObjectSpawn()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        RestoreValues();
    }
    //Must have, even left blank.
    public void OnObjectDespawn()
    {
    }
    //specifically for restoring some values, like health
    public void RestoreValues()
    {
        rotTimeCount = 0.0f;
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
    //using pool end

    private void Start()
    {
        poolManager = PoolManager.Instance;
    }

    void Update()
    {
        if(target== null)
        {
            //UpdateTarget();
            GetComponent<PooledObjectAttachment>().PutBackToPool();
            SpawnFX();
            return;
        }
        else
        {
            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            AimRotation(target);
            if (dir.magnitude <= distanceThisFrame )
            {
                HitTarget();
                return;
            }
        }

        
    }

    void UpdateTarget()
    {
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeSelf == true)
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
            target = nearestEnemy.transform;
            PrepareRotation(target);
        }
        else
        {
            target = null;
        }
    }
    private void PrepareRotation(Transform enemyTarget)
    {
        if(enemyTarget != null)
        {
            rotTimeCount = 0.0f;
            rotFormal = transform.forward;
            rotGoal = enemyTarget.position - transform.position;
        }
    }
    private void AimRotation(Transform enemyTarget)
    {
        if(enemyTarget != null)
        {
            if (rotTimeCount < 1.0f)
            {
                transform.forward = Vector3.Lerp(rotFormal, rotGoal, rotTimeCount);
                rotTimeCount += 10 * Time.deltaTime;//define the rotate speed
            }
            else
            {
                transform.forward = enemyTarget.position - transform.position;
            }
        }
        
    }

    void HitTarget()
    {
        //GameObject effectIns = Instantiate(hitEnemyFX, transform.position, transform.rotation);
        //Destroy(effectIns, 2f);
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
        GameObject go = poolManager.SpawnFromSubPool(hitEnemyFX.name.ToString(), transform);//This line needed for pooling
        go.transform.SetParent(GameObject.Find("PooledFX").transform, true);
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                float damageRatio = 1 - Vector3.Distance(collider.transform.position, transform.position)/(explosionRadius + affRadius);
                //Debug.Log("The damage ratio is: " + damageRatio);
                Damage(collider.transform, damageRatio);
            }
        }
    }

    void Damage(Transform enemy, float damageRatio)
    {
        //Debug.Log("Test phrase");

        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            float floatDamage = damage * damageRatio;
            //Debug.Log("The damage taken by AOE Missile is: " + (int)floatDamage);
            actualDamage = (int)floatDamage;
            e.TakeDamage(actualDamage);
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
