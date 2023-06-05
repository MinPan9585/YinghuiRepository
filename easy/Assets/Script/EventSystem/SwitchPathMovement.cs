using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchPathMovement : MonoBehaviour
{
    [Header("ID")]
    public int id;
    [Header("Movement")]
    public bool isRotate;
    public Vector3 altTrans;
    
    private Vector3 iniTrans;
    private bool alt;

    private void Start()
    {
        GameEvents.Instance.OnSwitchPath += SwitchPath;

        alt = true;
        if (isRotate)
        {
            iniTrans = transform.rotation.eulerAngles;
        }
        else
        {
            //iniTrans = transform.position;
            iniTrans = transform.localPosition;
        }
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnSwitchPath -= SwitchPath;
    }

    public void SwitchPath(int id)
    {
        if(id == this.id)
        {
            if (isRotate)
            {
                if (alt)
                    //transform.rotation = Quaternion.Euler(altTrans);
                    LeanTween.rotate(gameObject, altTrans, 1).setEase(LeanTweenType.easeOutQuad);
                else
                    LeanTween.rotate(gameObject, iniTrans, 1).setEase(LeanTweenType.easeOutQuad);
            }
            else
            {
                if (alt)
                    //transform.position = altTrans;
                    LeanTween.moveLocal(gameObject, altTrans, 1).setEase(LeanTweenType.easeOutQuad);
                else
                    LeanTween.moveLocal(gameObject, iniTrans, 1).setEase(LeanTweenType.easeOutQuad);
            }

            alt = !alt;
        }
    }
}
