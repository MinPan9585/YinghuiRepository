using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawn : MonoBehaviour
{
    public PoolManager poolManager;    //This line needed for pooling
    public GameObject cube;
    public float spawnRate = 5f;
    public string Name//This line needed for pooling
    {
        get { return cube.name; }
    }

    private void Start()
    {
        poolManager = PoolManager.Instance;//This line needed for pooling
    }

    private float timer = 0f;
    GameObject cubeInstance = null;
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(timer >= 1f/spawnRate)
        {
            Debug.Log(Name);
            cubeInstance = poolManager.SpawnFromSubPool(Name, transform);//This line needed for pooling
            cubeInstance.transform.position = transform.position + Random.onUnitSphere * 2;
            timer = 0f;
        }
    }

    public void OnButtonClear()
    {
        poolManager.ClearAllSubPool();//This line needed for pooling
    }

}
