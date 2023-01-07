using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    public float range = 15f;
    public string enemyTag = "Enemy";

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        
    }
    void UpdateTarget()
    {
        GameObject[] enemis = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemis)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
       if( distanceToEnemy<shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        
        }
    }
    if (nearestEnemy !=null&& shortestDitance<=range)
        {
        
        target=nearestEnemy.transform;
        }
    void Update()
    {
    if (target == null)
        return;

    }
     void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
