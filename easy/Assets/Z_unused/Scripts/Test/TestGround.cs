using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGround : MonoBehaviour
{
    public PoolManager poolManager;
    private void Start()
    {
        poolManager = PoolManager.Instance;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit the ground!");
            //poolManager.DespawnFromSubPool(collision.gameObject);
        }
    }
}
