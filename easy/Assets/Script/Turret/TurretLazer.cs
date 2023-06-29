using System;
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
    //public LineRenderer lazerLine;
    public GameObject LazerVFX;
    public Transform beacon_0;
    public Transform beacon_1;
    public float detectionRadius = 0.5f;
    private Vector3 detectDirection;
    private float maxDistance;
    public Animator myAnim;

    [Header("Enemy Detection")]
    public LayerMask enemyLayer;

    [Header("Change terrain ID")]
    public int id;

    [Header("SFX")]
    public string lazerSFX = "Phasor";
    [SerializeField] private int sfxID;
    private bool playingLoop = false;

    [Header("Tune")]
    [SerializeField] private float checkingRate = 0.5f;
    public float animationGap = 0.2f;
    private bool inAnimation = false;
    private void Start()
    {
        //beacon displacement
        detectDirection = beacon_1.position - beacon_0.position;
        maxDistance = Vector3.Magnitude(beacon_1.position - beacon_0.position);

        /*lazerLine.useWorldSpace = true;
        lazerLine.SetPosition(0, beacon_0.position);
        lazerLine.SetPosition(1, beacon_1.position);
        */

        //checking
        InvokeRepeating(nameof(CallBurn), 0f, checkingRate);
        //lazerLine.enabled = false;//remember to change it back to false

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
        while (time < 1.1)
        {
            detectDirection = beacon_1.position - beacon_0.position;
            maxDistance = Vector3.Magnitude(beacon_1.position - beacon_0.position);
            //lazerLine.SetPosition(0, beacon_0.position);
            //lazerLine.SetPosition(1, beacon_1.position);

            yield return null;
        }
    }

    void CallBurn()
    {
        StartCoroutine(BurnEnemy());
    }
    IEnumerator BurnEnemy()
    {
        RaycastHit[] hitColliders = Physics.SphereCastAll
            (beacon_0.position, detectionRadius, detectDirection, maxDistance, enemyLayer, QueryTriggerInteraction.Ignore);

        if (hitColliders.Length > 0)
        {
            if (!inAnimation)
            {
                inAnimation = true;
                Debug.Log(gameObject.name + ": Start Animation here");
                myAnim.SetBool("IsAttacking", true);

                yield return new WaitForSeconds(animationGap);
            }
            else { yield return null; }


            //lazerLine.enabled = true;
            LazerVFX.SetActive(true);

            if (!playingLoop)
            {
                playingLoop = true;
                sfxID = AudioManager.Instance.PlaySFXLoop(lazerSFX);
            }

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
            //lazerLine.enabled = false;
            LazerVFX.SetActive(false);

            if (playingLoop)
            {
                playingLoop = false;
                AudioManager.Instance.StopSFXLoop(sfxID);
            }
            if (inAnimation)
            {
                inAnimation = false;
                myAnim.SetBool("IsAttacking", false);
            }
            yield return null;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(beacon_0.position, beacon_1.position);
    }
}
