using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjectSpawner : MonoBehaviour
{
    private void Start()
    {
        //Subscribe the event
        GameEvents.Instance.OnSpawnObjectFromPool += OnSpawnPooledObject;
        GameEvents.Instance.spawnAPooledObject += SpawnAPooledObject;
    }

    private void OnDisable()
    {
        //Unsubscribe the event
        GameEvents.Instance.OnSpawnObjectFromPool -= OnSpawnPooledObject;
        GameEvents.Instance.spawnAPooledObject -= SpawnAPooledObject;

    }

    public void OnSpawnPooledObject(GameObject go, Transform positionTrans)
    {
        //Spawn from pool
        GameObject obj = PoolManager.Instance.SpawnFromSubPool(go.name.ToString(), positionTrans);
        obj.transform.SetParent(obj.transform, true);
        obj.transform.position = positionTrans.position;
        obj.transform.rotation = positionTrans.rotation;
    }

    public GameObject SpawnAPooledObject(GameObject go, Transform positionTrans)
    {
        return PoolManager.Instance.SpawnFromSubPool(go.name.ToString(), positionTrans);
    }


}
