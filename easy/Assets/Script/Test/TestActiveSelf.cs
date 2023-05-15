using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestActiveSelf : MonoBehaviour
{
    [SerializeField] private bool isActiveSelf = false;
    [SerializeField] private bool isActiveInHierarchy = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeActive", 1f, 1f);
        InvokeRepeating("CheckActive", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        //CheckActive();
    }
    private void ChangeActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    private void CheckActive()
    {
        isActiveSelf = gameObject.activeSelf;
        isActiveInHierarchy = gameObject.activeInHierarchy;
        Debug.Log("ActiveSelf is: " + isActiveSelf);
        Debug.Log("ActiveInHierachy is: " + isActiveInHierarchy);
        Debug.Log("\n");

    }
}
