using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretLazer : MonoBehaviour
{ 
    public int lazerDamage = 10;
    public float burnWait = 2f;
    public float burnDuration = 5f;
    public int burnDamage = 5;

    [Header("Need assign")]
    public LineRenderer lazerLine;
    public Transform beacon_0;
    public Transform beacon_1;
    public float detectionRadius = 0.5f;
    private Vector3 detectDirection;
    private float maxDistance;

    [Header("Enemy Detection")]
    public LayerMask enemyLayer;

    [Header("Change terrain ID")]
    public int id;

    [Header("SFX")]
    public string lazerSFX = "Buzzer";

    [Header("Tune")]
    [SerializeField] private float checkingRate = 0.5f;
    private void Start()
    {
        //beacon displacement
        detectDirection = beacon_1.position - beacon_0.position;
        maxDistance = Vector3.Magnitude(beacon_1.position - beacon_0.position);

        lazerLine.useWorldSpace = true;
        lazerLine.SetPosition(0, beacon_0.position);
        lazerLine.SetPosition(1, beacon_1.position);

        //checking
        InvokeRepeating(nameof(BurnEnemy), 0f, checkingRate);
        lazerLine.enabled = false;//remember to change it back to false

        //change terrain event
        GameEvents.Instance.OnSwitchPath += UpdateBeacon;
    }
    private void OnDisable()
    {
        GameEvents.Instance.OnSwitchPath -= UpdateBeacon;
        StopAllCoroutines();
    }
    private void UpdateBeacon(int id)
    {
        if (id == this.id)
        {
            StartCoroutine(BeaconMovement());
        }
    }
    IEnumerator BeaconMovement()
    {
        float time = 0;
        while(time < 1.1)
        {
            detectDirection = beacon_1.position - beacon_0.position;
            maxDistance = Vector3.Magnitude(beacon_1.position - beacon_0.position);
            lazerLine.SetPosition(0, beacon_0.position);
            lazerLine.SetPosition(1, beacon_1.position);
            yield return null;
        }
    }

    void BurnEnemy()
    {
        RaycastHit[] hitColliders = Physics.SphereCastAll
            (beacon_0.position, detectionRadius, detectDirection, maxDistance, enemyLayer, QueryTriggerInteraction.Ignore);

        if(hitColliders.Length > 0 )
        {
            lazerLine.enabled = true;
            AudioManager.Instance.PlaySFXLoop(lazerSFX);
            foreach (RaycastHit hit in hitColliders)
            {
                if (hit.transform.TryGetComponent<IBurn>(out IBurn eBurn))
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
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(beacon_0.position, beacon_1.position);
    }
}
