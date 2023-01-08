using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    [Header("Attribute")]
    public float range = 15f;
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnSpeed = 10f;
    public float fireRate =1f;
    private float fireCountdown = 0f;
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
   
    if (nearestEnemy !=null&& shortestDistance<=range)
        {
        
        target=nearestEnemy.transform;
        }else
        {
            target = null;
        }
    }
    void Update()
    {
    if (target == null)
        return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation =Quaternion.Lerp(partToRotate.rotation,lookRotation,Time.deltaTime*turnSpeed) .eulerAngles;
        partToRotate.rotation =Quaternion.Euler(0f,rotation.y,0f);
        if(fireCountdown<=0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;

        }
        fireCountdown -= Time.deltaTime;
    }
    void Shoot()
    {
        Debug.Log("SHOOT!");
    }
     void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
