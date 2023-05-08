using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPath01 : MonoBehaviour
{
    public GameObject rotationSwitch;
    public bool isLeft = true;

    private void OnMouseDown()
    {
        if (isLeft)
        {
            rotationSwitch.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            rotationSwitch.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        isLeft = !isLeft;
    }
}
