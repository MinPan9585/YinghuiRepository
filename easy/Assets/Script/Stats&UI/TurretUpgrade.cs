using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour
{
    [SerializeField]int _1to2;
    [SerializeField]int _2to3;

    private string _turretTag;
    public GameObject[] turretList1;
    public GameObject[] turretList2;
    public GameObject[] turretList3;

    private bool _level1 = true;
    private bool _level2 = false;
    private bool _level3 = false;

    private int _money;

    private static GameObject[] _Tlist;

    private RaycastHit hit;
    private Ray ray;
    Vector3 target;
    GameObject _curGameObject;

    [SerializeField] private GameObject UI;
    private void Start()
    {
        _money = PlayerStats.Money;
        _Tlist = turretList1;
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("TurretCat1"))
                {
                    UI.SetActive(true);
                }
                else if (hit.transform.gameObject.CompareTag("TurretCat2"))
                {
                    UI.SetActive(true);
                }
                else if (hit.transform.gameObject.CompareTag("TurretCat3"))
                {
                    UI.SetActive(true);
                }
            }
            target = hit.point;
            _curGameObject = hit.transform.gameObject;
            _turretTag = _curGameObject.tag;

            Debug.Log("鼠标世界坐标:" + target);
            Debug.Log("物体信息:" + _curGameObject);
        }

        switch (_turretTag)
        {
            case "TurretCat1":
                _Tlist = turretList1;
                break;
            case "TurretCat2":
                _Tlist = turretList2;
                break;
            case "TurretCat3":
                _Tlist = turretList3;
                break;
        }

        if (_Tlist[0].activeSelf)
        {
            _level1 = true;
            _level2 = false;
            _level3 = false;
        }
        else if (_Tlist[1].activeSelf)
        {
            _level1 = false;
            _level2 = true;
            _level3 = false;
        }
        else if (_Tlist[2].activeSelf)
        {
            _level1 = false;
            _level2 = false;
            _level3 = true;
        }
        
        // if (turretLevel1.activeSelf)
        // {
        //     _level1 = true;
        //     _level2 = false;
        //     _level3 = false;
        // }
        // else if (turretLevel2.activeSelf)
        // {
        //     _level1 = false;
        //     _level2 = true;
        //     _level3 = false;
        // }
        // else if(turretLevel3.activeSelf)
        // {
        //     _level1 = false;
        //     _level2 = false;
        //     _level3 = true;
        // }
    }

    private void FixedUpdate()
    {
        _money = PlayerStats.Money;
    }

    public void TurretLevelUp()
    {
        if (_level1 && _money >= _1to2)
        {
            _Tlist[0].SetActive(false);
            _Tlist[1].SetActive(true);
            PlayerStats.Money = PlayerStats.Money - _1to2;
        }
        else if (_level2 && _money >= _2to3)
        {
            _Tlist[1].SetActive(false);
            _Tlist[2].SetActive(true);
            PlayerStats.Money = PlayerStats.Money - _2to3;
        }
    }

    public void TurretLevelDown()
    {
        if (_level2)
        {
            _Tlist[1].SetActive(false);
            _Tlist[0].SetActive(true);
            PlayerStats.Money = PlayerStats.Money + _1to2;
        }
        else if (_level3)
        {
            _Tlist[2].SetActive(false);
            _Tlist[1].SetActive(true);
            PlayerStats.Money = PlayerStats.Money + _2to3;
        }
    }
}
