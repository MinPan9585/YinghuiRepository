using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Canvas myCanvas;
    // Start is called before the first frame update
    void Start()
    {
        myCanvas = this.GetComponent<Canvas>();
        myCanvas.worldCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(Camera.main.transform, Vector3.up);

    }
}
