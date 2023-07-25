using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public float timeFactor = 0.1f;
    private void OnMouseDown()
    {
        Time.timeScale = timeFactor;
    }
}
