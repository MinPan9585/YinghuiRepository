using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class BuildManager : MonoBehaviour
{
    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;
    //��ʾ��ǰѡ�����̨��Ҫ�������̨
    private TurretData selectedTurretData;
    public Text moneyText;

    public Animator moneyAnimator;

    int money = 1000;

    void ChangeMoney(int change=0)
    {
        money += change;
        moneyText.text = "��" + money;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject()==false)
            {
                //������̨�Ľ���
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if( mapCube.turretGo==null)
                    {
                        //���Դ���
                        if(money>selectedTurretData.cost)
                        {
                            // money -= selectedTurretData.cost;
                            ChangeMoney(-selectedTurretData.cost);
                            mapCube.BuildTurret(selectedTurretData.turretPrefab);
                        }
                    }
                    else
                    {
                        //T000��ʾǮ����
                        moneyAnimator.SetTrigger("Flicker");
                    }
                }
                else
                {
                    //T000��������
                }
            }
        }
    }

    public void OnLaserSelected(bool isOn)
    {
        if(isOn)
        {
            selectedTurretData = laserTurretData;
        }
    }
    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = missileTurretData;
        }
    }
    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = standardTurretData;
        }
    }

}
