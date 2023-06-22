using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPathTrigger : MonoBehaviour
{
    [Header("ID")]
    public int id;

    private RaycastHit hit;
    //private void OnMouseDown()
    //{
    //    SwitchPath(id);
    //}
    private void SwitchPath(int id)
    {
        Debug.Log("Button is pressed, the path should be switched");
        GameEvents.Instance.SwitchPath(id);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Default")))
            {
                SwitchPath(id);
            }
        }
    }
}

