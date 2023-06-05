using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPathTrigger : MonoBehaviour
{
    [Header("ID")]
    public int id;
    private void OnMouseDown()
    {
        SwitchPath(id);
    }
    private void SwitchPath(int id)
    {
        //Debug.Log("Button is pressed, the path should be switched");
        GameEvents.Instance.SwitchPath(id);
    }
}

