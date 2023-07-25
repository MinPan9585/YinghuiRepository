using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public PoolManager poolManager;

    private void Start()
    {
        poolManager = PoolManager.Instance;
    }

    public void SpawnAProjectile(GameObject prefab, Transform trans)
    {
        //This line needed for pooling
        GameObject obj = poolManager.SpawnFromSubPool(prefab.name.ToString(), trans);
    }
}
