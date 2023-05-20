using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDropper : MonoBehaviour
{
    public int id;
    public GameObject cube;
    void Start()
    {
        GameEvents.current.OnButClicked += OnCubeDrop;
    }

    public void OnCubeDrop(int id)
    {
        if(id == this.id)
        {
            GameObject go = Instantiate(cube);
            go.transform.position = transform.position;
        }
    }

    private void OnDisable()
    {
        GameEvents.current.OnButClicked -= OnCubeDrop;
    }

    //private void OnDestroy()
    //{
    //    GameEvents.current.OnButClicked -= OnCubeDrop;
    //}
}
