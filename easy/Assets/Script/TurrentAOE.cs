using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentAOE : MonoBehaviour
{
    // Attack range of tower
    public float attackRange = 2f;
    public int damage = 10;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    // Store enemies that can be hit
    public List<EnemyNav0519> enemiesInRange = new List<EnemyNav0519>();

    //Graphics
    public ParticleSystem fireParticle;
    void Start()
    {
        fireParticle= GetComponent<ParticleSystem>();
        InvokeRepeating("UpdateTarget",0f, 0.5f);
    }

    void FixedUpdate()
    {
        fireCountdown += Time.deltaTime;

        if (fireCountdown >= 1/fireRate && enemiesInRange.Count > 0)
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
                    EnemyNav0519 enemy = c.GetComponent<EnemyNav0519>();
                    if (!Physics.Raycast(transform.position, enemy.transform.position + new Vector3(0, 0.5f, 0) - transform.position,
                        (attackRange - 0.8f), ~3)) //~LayerMask.GetMask("Ground")
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
            foreach (EnemyNav0519 e in enemiesInRange)
            {
                e.TakeDamage(damage);
            }
        }
        fireParticle.Emit(100);

    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
