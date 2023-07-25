using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRight : MonoBehaviour
{
    private RaycastHit hit;
    private Ray ray;
    Vector3 target;
    GameObject _curGameObject;

    [SerializeField] private GameObject UI;
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    UI.SetActive(true);
                }
            }
            target = hit.point;
            _curGameObject = hit.transform.gameObject;

            Debug.Log("获取鼠标的世界坐标位置:" + target);
            Debug.Log("鼠标点击的物体信息:" + _curGameObject);
        }
    }
}
