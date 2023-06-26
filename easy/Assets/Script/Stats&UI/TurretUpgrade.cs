using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour
{
    [Header("Price")]
    [SerializeField]int aoe_1to2;
    [SerializeField]int aoe_2to3;
    [SerializeField]int shoot_1to2;
    [SerializeField]int shoot_2to3;
    [SerializeField]int lazer_1to2;
    [SerializeField]int lazer_2to3;
    [SerializeField]int stone_1to2;
    [SerializeField]int stone_2to3;
    private int _1to2 = 100;
    private int _2to3 = 150;
    
    [Header("AOE Properties")]
    [SerializeField] private int aoe_Damage;
    [SerializeField] private int aoe_SlowDownTime;
    [SerializeField] private float aoe_CD;
    
    [Header("Shoot Properties")]
    [SerializeField] private float shoot_Range;
    
    [Header("Stone Properties")]
    [SerializeField] private float stone_CD;
    
    [Header("Lazer Properties")]
    [SerializeField] private float lazer_CD;
    [SerializeField] private int lazer_BurnDamage;
    [SerializeField] private float lazer_BuranDuration;

    private string _turretTag;

    private bool _level1 = true;
    private bool _level2 = false;
    private bool _level3 = false;

    private int _money;

    private RaycastHit hit;
    private Ray ray;
    GameObject _curGameObject;
    
    [SerializeField] private GameObject UI;
    private void Start()
    {
        _money = PlayerStats.Money;
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit,Mathf.Infinity,LayerMask.GetMask("Turret")))
            {
                TurretSelected();
            }
        }

        switch (_turretTag)
        {
            case "Shoot":
                _1to2 = shoot_1to2;
                _2to3 = shoot_2to3;
                break;
            case "AOE":
                _1to2 = aoe_1to2;
                _2to3 = aoe_2to3;
                break;
            case "ThrowStone":
                _1to2 = stone_1to2;
                _2to3 = stone_2to3;
                break;
            case "Lazer":
                _1to2 = lazer_1to2;
                _2to3 = lazer_2to3;
                break;
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
            switch (_turretTag)
            {
                case "Shoot":
                    _curGameObject.GetComponent<TurretShoot>().range += shoot_Range;
                    break;
                case "AOE":
                    _curGameObject.GetComponent<TurrentAOE>().damage += aoe_Damage;
                    _curGameObject.GetComponent<TurrentAOE>().coolDown -= aoe_CD;
                    // _curGameObject.GetComponent<TurrentAOE>().attackRange += 10;
                    _curGameObject.GetComponent<TurrentAOE>().slowTime += aoe_SlowDownTime;
                    break;
                case "ThrowStone":
                    _curGameObject.GetComponent<TurretThrowStone>().coolDown -= stone_CD;
                    break;
                case "Lazer":
                    _curGameObject.GetComponent<TurretLazer>().burnDamage += lazer_BurnDamage;
                    _curGameObject.GetComponent<TurretLazer>().burnDuration += lazer_BuranDuration;
                    _curGameObject.GetComponent<TurretLazer>().burnWait -= lazer_CD;
                    break;
            }
            LevelStatus.Money = LevelStatus.Money - _1to2;
        }
        else if (_level2 && _money >= _2to3)
        {
            switch (_turretTag)
            {
                case "Shoot":
                    _curGameObject.GetComponent<TurretShoot>().range += shoot_Range;
                    break;
                case "AOE":
                    _curGameObject.GetComponent<TurrentAOE>().damage += aoe_Damage;
                    _curGameObject.GetComponent<TurrentAOE>().coolDown -= aoe_CD;
                    // _curGameObject.GetComponent<TurrentAOE>().attackRange += 10;
                    _curGameObject.GetComponent<TurrentAOE>().slowTime += aoe_SlowDownTime;
                    break;
                case "ThrowStone":
                    _curGameObject.GetComponentInParent<TurretThrowStone>().coolDown -= stone_CD;
                    break;
                case "Lazer":
                    _curGameObject.GetComponent<TurretLazer>().burnDamage += lazer_BurnDamage;
                    _curGameObject.GetComponent<TurretLazer>().burnDuration += lazer_BuranDuration;
                    _curGameObject.GetComponent<TurretLazer>().burnWait -= lazer_CD;
                    break;
            }
            LevelStatus.Money = LevelStatus.Money - _2to3;
        }
    }

    public void TurretLevelDown()
    {
        if (_level2)
        {
            switch (_turretTag)
            {
                case "Shoot":
                    _curGameObject.GetComponent<TurretShoot>().range -= shoot_Range;
                    break;
                case "AOE":
                    _curGameObject.GetComponent<TurrentAOE>().damage -= aoe_Damage;
                    _curGameObject.GetComponent<TurrentAOE>().coolDown += aoe_CD;
                    // _curGameObject.GetComponent<TurrentAOE>().attackRange -= 10;
                    _curGameObject.GetComponent<TurrentAOE>().slowTime -= aoe_SlowDownTime;
                    break;
                case "ThrowStone":
                    _curGameObject.GetComponent<TurretThrowStone>().coolDown += stone_CD;
                    break;
                case "Lazer":
                    _curGameObject.GetComponent<TurretLazer>().burnDamage -= lazer_BurnDamage;
                    _curGameObject.GetComponent<TurretLazer>().burnDuration -= lazer_BuranDuration;
                    _curGameObject.GetComponent<TurretLazer>().burnWait += lazer_CD;
                    break;
            }
            LevelStatus.Money = LevelStatus.Money + _1to2;
        }
        else if (_level3)
        {
            switch (_turretTag)
            {
                case "Shoot":
                    _curGameObject.GetComponent<TurretShoot>().range -= shoot_Range;
                    break;
                case "AOE":
                    _curGameObject.GetComponent<TurrentAOE>().damage -= aoe_Damage;
                    _curGameObject.GetComponent<TurrentAOE>().coolDown += aoe_CD;
                    // _curGameObject.GetComponent<TurrentAOE>().attackRange -= 10;
                    _curGameObject.GetComponent<TurrentAOE>().slowTime -= aoe_SlowDownTime;
                    break;
                case "ThrowStone":
                    _curGameObject.GetComponent<TurretThrowStone>().coolDown += stone_CD;
                    break;
                case "Lazer":
                    _curGameObject.GetComponent<TurretLazer>().burnDamage -= lazer_BurnDamage;
                    _curGameObject.GetComponent<TurretLazer>().burnDuration -= lazer_BuranDuration;
                    _curGameObject.GetComponent<TurretLazer>().burnWait += lazer_CD;
                    break;
            }
            LevelStatus.Money = LevelStatus.Money + _2to3;
        }
    }

    private void TurretSelected()
    {
        _curGameObject = hit.transform.gameObject;
        _turretTag = _curGameObject.tag;
        UI.SetActive(true);
        
        Debug.Log("物体信息:" + _curGameObject);
    }
}
