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
    //表示当前选择的炮台（要建造的炮台
    private TurretData selectedTurretData;
    public Text moneyText;

    public Animator moneyAnimator;

    int money = 1000;

    void ChangeMoney(int change=0)
    {
        money += change;
        moneyText.text = "￥" + money;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject()==false)
            {
                //开发炮台的建造
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if( mapCube.turretGo==null)
                    {
                        //可以创建
                        if(money>selectedTurretData.cost)
                        {
                            // money -= selectedTurretData.cost;
                            ChangeMoney(-selectedTurretData.cost);
                            mapCube.BuildTurret(selectedTurretData.turretPrefab);
                        }
                    }
                    else
                    {
                        //T000提示钱不够
                        moneyAnimator.SetTrigger("Flicker");
                    }
                }
                else
                {
                    //T000升级处理
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
