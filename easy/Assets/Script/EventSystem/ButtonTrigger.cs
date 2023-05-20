using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("mouse on ");
            GameEvents.current.ButClicked(0);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("mouse on ");
            GameEvents.current.ButClicked(1);
        }
    }
}
