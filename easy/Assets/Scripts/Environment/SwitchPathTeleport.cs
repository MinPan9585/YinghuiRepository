using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class SwitchPathTeleport : MonoBehaviour
{
    public int id;
    public Transform teleEnd0;
    public Transform teleEnd1;
    private Transform tele;
    private bool alt;
    private Vector3 end;

    void Start()
    {
        alt = true;
        tele = teleEnd0;
        GameEvents.Instance.OnSwitchPath += Teleport;
        end = GameObject.Find("End").transform.position;
    }
    private void OnDisable()
    {
        GameEvents.Instance.OnSwitchPath -= Teleport;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
            agent.Warp(tele.position);
            StartCoroutine(Relaunch(agent));
        }
    }
    IEnumerator Relaunch(NavMeshAgent agent)
    {
        yield return new WaitForSeconds(1);
        if (agent.gameObject.activeSelf)
        {
            agent.SetDestination(end);
        }
    }

    void Teleport(int id)
    {
        if(id == this.id)
        {
            //Debug.Log("Change tele");
            alt = !alt;
            if(alt) tele = teleEnd0;
            else tele = teleEnd1;
        }
    }
}
