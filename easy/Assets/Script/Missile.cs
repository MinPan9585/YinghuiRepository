using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;

    public int damage = 50;

    public float explosionRadius;
    public float affRadius = 0.5f;
    public GameObject hitEnemyFX;

    public string enemyTag = "Enemy";

    private bool hit = false;

    //Variables for testing
    [Header("Testing")]
    [SerializeField] private Transform currentEnemy;
    [SerializeField] private bool enemyIsActive;
    [SerializeField] private GameObject[] enemies;
    public void Seek(Transform _target)
    {
        target = _target;
    }
    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {
        if(target== null)
        {
            //UpdateTarget();
            Destroy(gameObject);
            return;
        }
        else
        {
            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            transform.LookAt(target);
            if (dir.magnitude <= distanceThisFrame && hit == false)
            {
                hit = true;
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
            currentEnemy = nearestEnemy.transform;
            //Debug.Log("This enemy target's activeSelf is:" + nearestEnemy.activeSelf);
            //Invoke("CheckAgain", 0.1f);
        }
        else
        {
            target = null;
        }

    }
    //private void CheckAgain()
    //{
    //    enemyIsActive = currentEnemy.gameObject.activeSelf;
    //    if (enemyIsActive == false)
    //    {
    //        Debug.Log("Wrong target");
    //    }
    //}

    void HitTarget()
    {
        GameObject effectIns = Instantiate(hitEnemyFX, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

        if(explosionRadius > 0f)
        {
            Explode();
        }
        else 
        {
            Damage(target, 1);
        }
        Destroy(gameObject);
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
            damage = (int)floatDamage;
            e.TakeDamage(damage);
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
