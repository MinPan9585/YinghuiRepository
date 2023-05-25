using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObjectSpawner : MonoBehaviour
{
    public GameObject prefab;
    [SerializeField] private GameObject obj;

    private void Start()
    {
        GameObject go = PoolManager.Instance.SpawnFromSubPool(prefab.name.ToString(), transform);
    }
}
