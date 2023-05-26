using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    [Header("Test")]
    [SerializeField]private GameObject ob;

    private static GameEvents _eventManager;
    public static GameEvents Instance 
    {
        get 
        { 
            if (_eventManager == null)
            {
                GameObject.Find("GameMaster").AddComponent<GameEvents>();
            }
            return _eventManager; 
        } 
    }

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

    public event Action OnDie;
    public void LoseTheGame()
    {
        OnDie?.Invoke();
    }
}
