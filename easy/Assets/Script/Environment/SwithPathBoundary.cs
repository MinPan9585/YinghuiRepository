using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SwithPathBoundary : MonoBehaviour
{
    [Header("Test")]
    [SerializeField] private BoxCollider bCollider;
    [SerializeField] private GameObject boxTrans;
    [SerializeField] private Vector3 resetPoint;
    private Vector3 end;

    private void Start()
    {
        end = GameObject.Find("End").transform.position;

        bCollider = transform.parent.GetComponent<BoxCollider>();
        resetPoint = bCollider.bounds.center;

        //below two lines are for testing
        //boxTrans = new GameObject();
        //boxTrans.transform.position= resetPoint;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
        {

            if (other.TryGetComponent<KnockedOff>(out KnockedOff minion))
            {
                minion.KnockOff();
            }
            else
            {
                if (bCollider != null)
                {
                    agent.Warp(resetPoint);
                    agent.SetDestination(end);
                }
            }
        }
    }
}
