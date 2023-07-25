using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentAOE : MonoBehaviour
{
    [Header("Parameters")]
    public float attackRange = 2f;
    public int damage = 10;
    public float coolDown = 1f;
    public int slowTime = 2;
    private float fireCountdown = 0f;
    public LayerMask layerToBlock;

    [Header("Graphics")]
    public GameObject fireParticle;
    [Header("SFX")]
    public string hitSFX = "Buzzer";

    [Header("Ignore, enemy in range")]
    public List<EnemyBase> enemiesInRange = new List<EnemyBase>();
    
    void Start()
    {
        InvokeRepeating("UpdateTarget",0f, 0.5f);
    }

    void FixedUpdate()
    {
        fireCountdown += Time.deltaTime;

        if (fireCountdown >= coolDown && enemiesInRange.Count > 0)
        {
            fireCountdown = 0;
            AOEDamage();
        }
    }

    void UpdateTarget()
    {
        enemiesInRange.Clear();

        // Check for enemies/obstacles within attack range 
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);

        if (hitColliders.Length > 0)
        {
            foreach (Collider c in hitColliders)
            {
                if (c.tag == "Enemy")
                {
                    EnemyBase enemy = c.GetComponent<EnemyBase>();
                    if (!Physics.Raycast(transform.position, c.transform.position + new Vector3(0, 0.5f, 0) - transform.position,
                        (attackRange - 0.8f), layerToBlock)) //~LayerMask.GetMask("Ground")
                    {
                        //Debug.Log("Raycast hit something");
                        enemiesInRange.Add(enemy);
                    }
                }
            }
        }
    }

    // Attack all enemies in enemiesInRange
    private void AOEDamage()
    {
        if (enemiesInRange.Count > 0)
        {
            foreach (EnemyBase e in enemiesInRange)
            {
                e.TakeDamage(damage);
                e.SlowDown(slowTime);
            }
        }
        GameObject go = PoolManager.Instance.SpawnFromSubPool(fireParticle.name.ToString(), transform);
        go.transform.SetParent(transform, false);
        go.transform.SetPositionAndRotation(transform.position, transform.rotation);
        AudioManager.Instance.PlaySFX(hitSFX);
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
