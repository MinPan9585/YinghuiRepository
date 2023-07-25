using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour, ITakeDamage, ISlowDown
{
    [Header("Enemy parameters")]
    public float speed = 3;

    public float startHealth = 100f;
    private float health;
    [Tooltip("Actual damage is reduced by armor%")]
    public float armor = 5;

    public int value = 50;
    public int deathCount = 1;

    private Transform start;
    private Transform target;

    [Header("Public accessible parameters")]
    public NavMeshAgent agent;
    public float remainingDistance;

    [Header("Needs assign")]
    public Image healthBar;

    [Header("FX")]
    public string dieSFX = "Buzzer";
    public GameObject dieVFX;

    [Header("Other coding parameters, ignore")]
    public bool isdead = false;

    private NavMeshPath path;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        start = GameObject.Find("Start").transform;
        agent.Warp(start.position);
        target = GameObject.Find("End").transform;
        agent.SetDestination(target.position);
        path = new NavMeshPath();
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
        //transform.localPosition = Vector3.zero;
        //transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        CancelInvoke();
        StopAllCoroutines();
    }
    #endregion

    public void TakeDamage(int amount)
    {
        if (!isdead)
        {
            //armor
            amount = (int)(amount * (1 - armor * 0.01));

            health -= amount;

            healthBar.fillAmount = health / startHealth;

            if (health <= 0 && !isdead)
            {
                isdead = true;
                Die();
            }
        } 
    }

    public void SlowDown(int time)
    {
        if (!isdead)
        {
            StartCoroutine(SlowSlow(time));
        }
    }
    IEnumerator SlowSlow(int time)
    {
        float originalSpeed = agent.speed;
        agent.speed = agent.speed / 2;
        yield return new WaitForSeconds(time);
        agent.speed = originalSpeed;
    }


    public void Die()
    {
        LevelStatus.Money += value;
        LevelStatus.EnemyBaseList.Remove(this);
        GameEvents.Instance.UpdateDisplay();

        GameObject go = PoolManager.Instance.SpawnFromSubPool(dieVFX.name.ToString(), transform);
        go.transform.SetParent(GameObject.Find("PooledPrefabs").transform, true);
        go.transform.SetPositionAndRotation(transform.position, transform.rotation);
        AudioManager.Instance.PlaySFX(dieSFX);

        GetComponent<PooledObjectAttachment>().PutBackToPool();
    }

    public void RecalculatePath()
    {
        if (!isdead)
        {
            agent.SetDestination(target.position);
        }
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
        isdead = true;
        LevelStatus.Lives -= deathCount;
        LevelStatus.EnemyBaseList.Remove(this);
        GameEvents.Instance.UpdateDisplay();
        GetComponent<PooledObjectAttachment>().PutBackToPool();
    }
}
