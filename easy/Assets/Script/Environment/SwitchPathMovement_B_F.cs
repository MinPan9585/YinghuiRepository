using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchPathMovement_B_F : MonoBehaviour
{
    [Header("ID")]
    public int id;
    [Header("Movement")]
    public bool isRotate;
    public float duration = 1f;
    public Vector3 altTrans;

    private Vector3 iniTrans;
    private bool alt;

    private void Start()
    {
        GameEvents.Instance.OnSwitchPath += Back_Forth;

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
        GameEvents.Instance.OnSwitchPath -= Back_Forth;
    }

    public void Back_Forth(int id)
    {
        if(id == this.id)
        StartCoroutine(B_F());
    }
    IEnumerator B_F()
    {
        SwitchPath(id);
        yield return new WaitForSeconds(duration);
        SwitchPath(id);
    }

    private void SwitchPath(int id)
    {
        //Debug.Log("DOOOOOO");
        if (id == this.id)
        {
            if (isRotate)
            {
                if (alt)
                    //transform.rotation = Quaternion.Euler(altTrans);
                    LeanTween.rotate(gameObject, altTrans, 0.1f).setEase(LeanTweenType.easeOutQuad);
                else
                    LeanTween.rotate(gameObject, iniTrans, 0.1f).setEase(LeanTweenType.easeOutQuad);
            }
            else
            {
                if (alt)
                    //transform.position = altTrans;
                    LeanTween.moveLocal(gameObject, altTrans, 0.1f).setEase(LeanTweenType.easeOutQuad);
                else
                    LeanTween.moveLocal(gameObject, iniTrans, 0.1f).setEase(LeanTweenType.easeOutQuad);
            }

            alt = !alt;

        }
    }
}
