using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBurned : MonoBehaviour, IBurn
{
    [Header("Burn")]
    [SerializeField] private float burnStep = 0;
    [SerializeField] private float burnCount = 0;
    private float burningCount = 0;
    [SerializeField] bool isBurning = false;

    private EnemyBase enemyBase;

    [Header("Visual")]
    public GameObject burnVFX;

    private void Start()
    {
        enemyBase= GetComponent<EnemyBase>();
    }
    private void OnDisable()
    {
        burnCount = 0;
        burnStep = 0;
        burningCount = 0;
        CancelInvoke();
        StopAllCoroutines();
        isBurning = false;
    }

    public void Burn(float burnWait, float burnDuration, float timePassed, int lazerDamage, int burnDamage)
    {
        burnCount += timePassed;
        if (burnCount > burnStep)
        {
            enemyBase.TakeDamage(lazerDamage);
            burnStep += 1;
        }
        if (burnCount > burnWait)
        {
            if (!isBurning)
            {
                burnCount = 0;
                burnStep = 0;

                isBurning = true;
                burningCount = 0;

                //Debug.Log("Play flame effects here");
                GameObject go = PoolManager.Instance.SpawnFromSubPool(burnVFX.name.ToString(), transform);//This line needed for pooling
                go.transform.SetParent(transform, false);
                go.transform.SetPositionAndRotation(transform.position, transform.rotation);

                StartCoroutine(SufferBurn(burnDuration, burnDamage));
            }
        }
    }
    private IEnumerator SufferBurn(float burnDuration, int burnDamage)
    {
        while(burningCount < burnDuration)
        {
            enemyBase.TakeDamage(burnDamage);
            burningCount++;
            //Debug.Log("Taken flame damage " + burningCount);
            yield return new WaitForSeconds(1);
        }

        burningCount = 0;
        StopAllCoroutines();
        isBurning = false;


    }
}
