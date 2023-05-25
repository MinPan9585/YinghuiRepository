using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    [Header("Object to spawn from pool")]
    public GameObject objectFromPool;

    [Header("Test")]
    [SerializeField] private GameObject go;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("mouse on for new delegate pooling");
            go = GameEvents.eventManager.ButClicked(objectFromPool, transform);
        }

        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("mouse on for pooling");
            GameEvents.eventManager.MiddleMouseCliked(objectFromPool, transform);
        }
    }
}
