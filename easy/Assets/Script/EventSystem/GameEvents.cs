using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<int> OnButClicked;
    public void ButClicked(int id)
    {
        if (OnButClicked != null)
        {
            OnButClicked(id);
        }
    }
}
