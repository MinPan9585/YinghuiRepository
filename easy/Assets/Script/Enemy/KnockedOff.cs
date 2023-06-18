using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KnockedOff : MonoBehaviour
{
    public float upForce = 6f;
    public float sideForce = 4f;
    private void OnDisable()
    {
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<SphereCollider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    public void KnockOff()
    {
        GetComponent<EnemyBase>().isdead = true;
        GetComponent<NavMeshAgent>().enabled=false;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic= false;

        float xForce = Random.Range(-sideForce, sideForce);
        float yForce = Random.Range(upForce / 2f, upForce);
        float zForce = Random.Range(-sideForce, sideForce);

        Vector3 force = new Vector3(xForce, yForce, zForce);

        GetComponent<Rigidbody>().velocity= force;

        Invoke(nameof(GoDie), 2f);
        
    }

    void GoDie()
    {
        GetComponent<EnemyBase>().Die();
    }
}
