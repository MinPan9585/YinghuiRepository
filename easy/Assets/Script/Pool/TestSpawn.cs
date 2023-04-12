using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawn : MonoBehaviour
{
    public PoolManager poolManager;
    public GameObject cube;
    public string Name
    {
        get { return cube.name; }
    }

    private void Start()
    {
        poolManager = PoolManager.Instance;
    }

    private float timer = 0f;
    GameObject cubeInstance = null;
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(timer >= 0.5f)
        {
            cubeInstance = poolManager.SpawnFromSubPool(Name, transform);
            cubeInstance.transform.position = transform.position + Random.onUnitSphere * 2;
        }
        if (timer >= 4f)
        {
            poolManager.ClearAllSubPool();
            timer= 0f;
        }
    }

    
}
