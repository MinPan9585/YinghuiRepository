using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SwitchPathFreeze : MonoBehaviour
{
    [Header("ID")]
    public int id;
    [Header("Test")]
    public float freezeTime = 1f;
    [SerializeField] private List<NavMeshAgent> agents;
    private BoxCollider bColidder;
    //[SerializeField] private Vector3 relativeMoveDir;
    [Header("No need to assign")]
    [SerializeField] private GameObject boundaryCheck;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if(other.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
            {
                agents.Add(agent);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
            {
                agents.Remove(agent);
            }
        }
    }

    private void Start()
    {
        bColidder = GetComponent<BoxCollider>();
        //relativeMoveDir = new Vector3(bColidder.center.x, 0, bColidder.center.z).normalized;
        GameEvents.Instance.OnSwitchPath += SwitchPath;
        boundaryCheck = GetComponentInChildren<SwithPathBoundary>().gameObject;

    }

    private void OnDisable()
    {
        GameEvents.Instance.OnSwitchPath -= SwitchPath;
    }

    public void SwitchPath(int id)
    {
        if (id == this.id)
        {
            StartCoroutine(HandleBoundary());
            foreach (NavMeshAgent agent in agents)
            {
                StartCoroutine(AgentStopGo(agent));
            }
        }
    }

    IEnumerator AgentStopGo(NavMeshAgent agent)
    {
        agent.isStopped = true;
        //agent.Move(relativeMoveDir);
        yield return new WaitForSeconds(freezeTime);
        agent.isStopped = false;
    }

    IEnumerator HandleBoundary()
    {
        boundaryCheck.SetActive(false);
        yield return new WaitForSeconds(freezeTime);
        boundaryCheck.SetActive(true);
    }
}
