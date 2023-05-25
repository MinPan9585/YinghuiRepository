using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjectSpawner : MonoBehaviour
{
    //This variable is for pooling
    private PoolManager poolManager;

    private void Start()
    {
        poolManager = PoolManager.Instance;
        //Subscribe the event
        GameEvents.eventManager.OnSpawnObjectFromPool += OnSpawnPooledObject;
        GameEvents.eventManager.spawnAPooledObject += SpawnAPooledObject;
    }

    private void OnDisable()
    {
        //Unsubscribe the event
        GameEvents.eventManager.OnSpawnObjectFromPool -= OnSpawnPooledObject;
        GameEvents.eventManager.spawnAPooledObject -= SpawnAPooledObject;

    }

    public void OnSpawnPooledObject(GameObject go, Transform positionTrans)
    {
        //Spawn from pool
        GameObject obj = poolManager.SpawnFromSubPool(go.name.ToString(), positionTrans);
        obj.transform.SetParent(obj.transform, true);
        obj.transform.position = positionTrans.position;
        obj.transform.rotation = positionTrans.rotation;
    }

    public GameObject SpawnAPooledObject(GameObject go, Transform positionTrans)
    {
        return poolManager.SpawnFromSubPool(go.name.ToString(), positionTrans);
    }


}
