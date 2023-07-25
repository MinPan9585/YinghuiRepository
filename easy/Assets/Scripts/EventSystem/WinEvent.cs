using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinEvent : MonoBehaviour
{
    private void Start()
    {
        GameEvents.Instance.OnWin += WinWinWin;
    }
    private void OnDisable()
    {
        GameEvents.Instance.OnWin -= WinWinWin;
    }
    public void WinWinWin()
    {
        Debug.Log("Level won! Now disable other UI, and enable Winning UI");
    }
}
