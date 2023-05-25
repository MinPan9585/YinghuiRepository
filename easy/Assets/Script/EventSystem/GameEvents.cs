using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    [Header("Test")]
    [SerializeField]private GameObject ob;

    public static GameEvents _eventManager;

    public static GameEvents eventManager { get { return _eventManager; } }

    private void Awake()
    {
        if (_eventManager != null && _eventManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _eventManager = this;
        }
    }    

    public delegate GameObject OnSpawnAPooledObject(GameObject go, Transform positionTrans);
    public event OnSpawnAPooledObject spawnAPooledObject;
    public GameObject ButClicked(GameObject go, Transform positionTrans)
    {
        ob = spawnAPooledObject?.Invoke(go, positionTrans);
        return ob;
    }


    public event Action<GameObject, Transform> OnSpawnObjectFromPool;
    public void MiddleMouseCliked(GameObject go, Transform positionTrans)
    {
        OnSpawnObjectFromPool?.Invoke(go, positionTrans);
    }
}
