using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyBase>(out EnemyBase go))
        {
            go.EndPath();
        }
    }
}
