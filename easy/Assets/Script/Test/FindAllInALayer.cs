using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
public class FindAllInALayer : MonoBehaviour
{
	public List<GameObject> objsInALayer = new List<GameObject>();
    public LayerMask layerToFind;
    private void Start()
    {
        GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();
        for (int i = 0; i < allGameObjects.Length; i++)
        {
            if (layerToFind == (layerToFind | (1 << allGameObjects[i].layer)))
            {
                objsInALayer.Add(allGameObjects[i]);
                Debug.Log("Found one");
            }
        }
    }
    
}
