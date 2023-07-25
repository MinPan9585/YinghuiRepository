using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    private GameObject ob;

    #region Singleton
    private static GameEvents _eventManager;
    public static GameEvents Instance 
    {
        get 
        { 
            //if (_eventManager == null)
            //{
            //    GameObject.Find("GameMaster").AddComponent<GameEvents>();
            //}
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
    #endregion
    #region TestToPassParameter
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
    #endregion

    public event Action OnDie;
    public void LoseTheGame()
    {
        OnDie?.Invoke();
    }

    public event Action OnWin;
    public void WinTheGame()
    {
        OnWin?.Invoke();
    }

    public event Action<int> OnSwitchPath;
    public void SwitchPath(int id) 
    {
        //Debug.Log("Event is called");
        OnSwitchPath?.Invoke(id); 
    }

    public event Action OnSpawnRound;
    public void SpawnRound()
    {
        //Debug.Log("Event is called");
        OnSpawnRound?.Invoke();
    }

    public event Action OnUpdateDisplay;
    public void UpdateDisplay()
    {
        OnUpdateDisplay?.Invoke();
    }
    
    public event Action<bool> OnMenuDisplay;
    public void MenuDisplay(bool pause)
    {
        OnMenuDisplay?.Invoke(pause);
    }
    
    public event Action<List<Wave>> OnPreviewEnemy;
    public void PreviewEnemy(List<Wave> waves)
    {
        OnPreviewEnemy?.Invoke(waves);
    }
    public event Action OnRoundSpawned;
    public void RoundSpawned()
    {
        OnRoundSpawned?.Invoke();
    }

}
