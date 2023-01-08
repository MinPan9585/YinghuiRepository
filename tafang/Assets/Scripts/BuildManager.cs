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

    public Animation moneyAnimator;

    private int money = 1000;
    void ChangeMoney(int change=0)
    {
        money += change;
        moneyText.text = "��" + money;
    }

    public MapCube MapCube { get; private set; }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject()==false)
            {
                //������̨�Ľ���
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
               bool isCollider= Physics.Raycast(ray,out hit, 1000,new LayerMask(), GetMask("MapCube"));
                if (isCollider)
                {
                     MapCube = hit.collider.GetComponent<MapCube>();
                    if( MapCube.turretGo==null)
                    {
                        //���Դ���
                        if(money>selectedTurretData.cost)
                        {
                            // money -= selectedTurretData.cost;
                            ChangeMoney(-selectedTurretData.cost);
                            MapCube.BuildTurret(selectedTurretData.turretPrefab);
                            
                        }
                    }
                    else
                    {

                        //T000��ʾǮ����
                        //moneyAnimator.SetTrigger("Flicker");
                    }
                }
                else
                {
                    //T000��������
                }
            }
        }
    }

    private QueryTriggerInteraction GetMask(string v)
    {
        throw new NotImplementedException();
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
