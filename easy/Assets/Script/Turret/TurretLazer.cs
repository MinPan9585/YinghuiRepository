using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretLazer : MonoBehaviour
{
    public LineRenderer lazerLine;
    public int lazerDamage = 10;
    public float burnWait = 2f;
    public float burnDuration = 5f;
    public int burnDamage = 5;

    [Header("Enemy Detection")]
    public LayerMask enemyLayer;
    public Vector3 collisionScale = new (2,1,1);

    [Header("Tune")]
    [SerializeField] private float checkingRate = 0.5f;
    private void Start()
    {
        InvokeRepeating(nameof(BurnEnemy), 0f, checkingRate);
        lazerLine.enabled = true;
    }

    void BurnEnemy()
    {
        Collider[] hitColliders = Physics.OverlapBox
            (transform.position, collisionScale, Quaternion.identity, enemyLayer);

        if(hitColliders.Length > 0 )
        {
            lazerLine.enabled = true;
            foreach (Collider col in hitColliders)
            {
                if (col.TryGetComponent<IBurn>(out IBurn eBurn))
                {
                    eBurn.Burn(burnWait, burnDuration, checkingRate, lazerDamage, burnDamage);
                }
            }
        }
        else
        {
            lazerLine.enabled = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, collisionScale * 2);
    }
}
