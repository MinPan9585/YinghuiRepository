using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchPathMovement : MonoBehaviour
{
    public NavMeshSurface surface;

    [Header("ID")]
    public int id;
    [Header("Movement")]
    public bool isRotate;
    public bool isChangeMat;

    public Vector3 altTrans;
    public Material mat1;
    public Material mat2;

    private Material iniMat;
    private Vector3 iniTrans;
    private bool alt;
    [Header("SFX")]
    public string moveSFX = "Buzzer";

    private void Start()
    {
        GameEvents.Instance.OnSwitchPath += SwitchPath;

        alt = true;
        if (isRotate)
        {
            iniTrans = transform.rotation.eulerAngles;
        }
        //else if (isChangeMat)
        //{
        //    iniMat = mat1;
        //}
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
        


        if (id == this.id)
        {
            InvokeRepeating(nameof(SwitchNavMesh), 0f, 0.02f);
            Invoke(nameof(CancelInvoke), 1f);
            AudioManager.Instance.PlaySFX(moveSFX);
            if (isRotate)
            {
                if (alt)
                    //transform.rotation = Quaternion.Euler(altTrans);
                    LeanTween.rotate(gameObject, altTrans, 1).setEase(LeanTweenType.easeOutQuad);
                else
                    LeanTween.rotate(gameObject, iniTrans, 1).setEase(LeanTweenType.easeOutQuad);
            }
            else if (isChangeMat)
            {
                if (alt)
                    this.GetComponent<MeshRenderer>().material = mat2;
                else
                    this.GetComponent<MeshRenderer>().material = mat1;
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

    void SwitchNavMesh()
    {
        surface = transform.parent.GetComponent<NavMeshSurface>();
        surface.UpdateNavMesh(surface.navMeshData);
    }
}
