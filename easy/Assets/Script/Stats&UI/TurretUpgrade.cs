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
    public GameObject[] turretList4;
    public GameObject[] turretList5;
    public GameObject[] turretList6;

    private bool _level1 = true;
    private bool _level2 = false;
    private bool _level3 = false;

    private int _money;

    private static GameObject[] _Tlist;

    private RaycastHit hit;
    private Ray ray;
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
            if (Physics.Raycast(ray, out hit,Mathf.Infinity,LayerMask.GetMask("Turret")))
            {
                if (hit.transform.gameObject.CompareTag("TurretCat1"))
                {
                    UI.SetActive(true);
                    TurretSelected();
                }
                else if (hit.transform.gameObject.CompareTag("TurretCat2"))
                {
                    UI.SetActive(true);
                    TurretSelected();
                }
                else if (hit.transform.gameObject.CompareTag("TurretCat3"))
                {
                    UI.SetActive(true);
                    TurretSelected();
                }
                else if (hit.transform.gameObject.CompareTag("TurretCat4"))
                {
                    UI.SetActive(true);
                    TurretSelected();
                }
                else if (hit.transform.gameObject.CompareTag("TurretCat5"))
                {
                    UI.SetActive(true);
                    TurretSelected();
                }
                else if (hit.transform.gameObject.CompareTag("TurretCat6"))
                {
                    UI.SetActive(true);
                    TurretSelected();
                }
            }
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
            case "TurretCat4":
                _Tlist = turretList4;
                break;
            case "TurretCat5":
                _Tlist = turretList5;
                break;
            case "TurretCat6":
                _Tlist = turretList6;
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
    }

    private void FixedUpdate()
    {
        _money = LevelStatus.Money;
    }

    public void TurretLevelUp()
    {
        if (_level1 && _money >= _1to2)
        {
            _Tlist[0].SetActive(false);
            _Tlist[1].SetActive(true);
            LevelStatus.Money = LevelStatus.Money - _1to2;
        }
        else if (_level2 && _money >= _2to3)
        {
            _Tlist[1].SetActive(false);
            _Tlist[2].SetActive(true);
            LevelStatus.Money = LevelStatus.Money - _2to3;
        }
    }

    public void TurretLevelDown()
    {
        if (_level2)
        {
            _Tlist[1].SetActive(false);
            _Tlist[0].SetActive(true);
            LevelStatus.Money = LevelStatus.Money + _1to2;
        }
        else if (_level3)
        {
            _Tlist[2].SetActive(false);
            _Tlist[1].SetActive(true);
            LevelStatus.Money = LevelStatus.Money + _2to3;
        }
    }

    private void TurretSelected()
    {
        _curGameObject = hit.transform.gameObject;
        _turretTag = _curGameObject.tag;

        // Debug.Log("鼠标世界坐标:" + target);
        //Debug.Log("物体信息:" + _curGameObject);
    }
}
