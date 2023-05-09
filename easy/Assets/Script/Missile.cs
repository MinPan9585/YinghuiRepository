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
    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if(target== null)
        {
            UpdateTarget();
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame && hit == false)
        {
            hit = true;
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

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
                Debug.Log("The damage ratio is: " + damageRatio);
                Damage(collider.transform.parent, damageRatio);
            }
        }
    }

    void Damage(Transform enemy, float damageRatio)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            float floatDamage = damage * damageRatio;
            Debug.Log("The damage taken is: " + (int)floatDamage);
            e.TakeDamage(damage);
        }
        
        //Destroy(enemy.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
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

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
            Destroy(gameObject);
        }

    }
}
