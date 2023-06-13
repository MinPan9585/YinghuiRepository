using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour, ITakeDamage
{
    [Header("Enemy parameters")]
    public float speed = 3;

    public float startHealth = 100f;
    [SerializeField] private float health;

    public int value = 50;

    private Transform start;
    private Transform target;
    public NavMeshAgent agent;

    [Header("Needs assign")]
    public Image healthBar;

    public bool isdead = false;

    

    [Header("Test")]
    [SerializeField] private float remainingDistance;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        start = GameObject.Find("Start").transform;
        agent.Warp(start.position);
        target = GameObject.Find("End").transform;
        agent.SetDestination(target.position);
    }

    #region Pool
    //Must have, even left blank. Also, put everything in Start() function here
    public void OnObjectSpawn()
    {
        agent = GetComponent<NavMeshAgent>();
        start = GameObject.Find("Start").transform;
        agent.Warp(start.position);
        //agent.enabled = true;
        RestoreValues();
        target = GameObject.Find("End").transform;
        //Debug.Log(target.name);

        agent.SetDestination(target.position);
    }

    //Must have, now have some issues, just leave it blank.
    public void OnObjectDespawn() { }

    //specifically for restoring some values, like health
    public void RestoreValues()
    {
        //reset die only once bool
        isdead = false;

        //reset health
        health = startHealth;
        healthBar.fillAmount = health / startHealth;
        //healthBar.fillAmount = health / startHealth;


    }
    private void OnDisable()
    {
        //reset movement status (recommended for every pooled object)
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        CancelInvoke();
        StopAllCoroutines();
    }
    #endregion

    public void TakeDamage(int amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isdead)
        {
            isdead = true;
            Die();
        }
    }

    

    public void Die()
    {
        LevelStatus.Money += value;
        GameEvents.Instance.UpdateDisplay();
        GetComponent<PooledObjectAttachment>().PutBackToPool();
    }

    public float GetPathRemainingDistance(bool performant)
    {
        if(performant)
        {
            return Vector3.Distance(transform.position, target.position);
        }
        else
        {
            if (agent.pathPending ||
            agent.pathStatus == NavMeshPathStatus.PathInvalid ||
            agent.path.corners.Length == 0)
                return -1f;

            float distance = 0.0f;
            for (int i = 0; i < agent.path.corners.Length - 1; ++i)
            {
                distance += Vector3.Distance(agent.path.corners[i], agent.path.corners[i + 1]);
            }

            return distance;
        }
    }

    public void EndPath()
    {
        LevelStatus.Lives--;
        GameEvents.Instance.UpdateDisplay();
        GetComponent<PooledObjectAttachment>().PutBackToPool();
    }
}
