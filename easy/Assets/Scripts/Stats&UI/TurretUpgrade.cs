using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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
    [SerializeField] float shoot_CD = 1f;
    [SerializeField] float shoot_turnSpeed = 1f;
    
    [Header("Stone Properties")]
    [SerializeField] private float stone_CD;
    
    [Header("Lazer Properties")]
    [SerializeField] private float lazer_CD;
    [SerializeField] private int lazer_BurnDamage;
    [SerializeField] private float lazer_BuranDuration;
    
    [Header("SFX")]
    public string turretSelected = "Selected";
    public string turretLevelUp = "Up";
    public string turretLevelDown = "Down";
    private int sfxID;
    private bool playingLoop = false;

    private int _curlevel;
    private TurretLevel _turretLevel;
    private TurretShoot _turretShoot;
    private TurrentAOE _turrentAoe;
    private TurretThrowStone _turretThrowStone;
    private TurretLazer _turretLazer;
    
    private string _turretTag;

    private int _money;

    private RaycastHit hit;
    private Ray ray;
    GameObject _curGameObject;
    
    [Header("UI")]
    [SerializeField] private GameObject UI;
    public TextMeshProUGUI upgradeMoney;
    public TextMeshProUGUI downgradeMoney;
    public GameObject UpgradeButtonUI;
    public GameObject DowngradeButtonUI;
    
    private void Start()
    {
        _money = PlayerStats.Money;
    }

    public void Update()
    {
        if(Input.GetMouseButtonUp(0)&& !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit,Mathf.Infinity,LayerMask.GetMask("Turret")))
            {
                TurretSelected();
            }
            else
            {
                CloseUpgradeMenu();
            }
        }

        if (_curlevel == 1)
        {
            DowngradeButtonUI.SetActive(false);
        }
        else
        {
            DowngradeButtonUI.SetActive(true);
        }

        if (_curlevel == 3)
        {
            UpgradeButtonUI.SetActive(false);
        }
        else
        {
            UpgradeButtonUI.SetActive(true);
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
        if (_curlevel == 1)
        {
            upgradeMoney.text = "-" + _1to2.ToString();
            downgradeMoney.text = "N/A";
        }
        else if (_curlevel == 2)
        {
            upgradeMoney.text = "-" + _2to3.ToString();
            downgradeMoney.text = "+" + _1to2.ToString();
        }
        else if (_curlevel == 3)
        {
            upgradeMoney.text = "N/A";
            downgradeMoney.text = "+" + _2to3.ToString();
        }
    }

    public void UpgradeButton()
    {
        if (_curlevel >= 3)
        {
            Debug.Log("The turret is max level!");
        }
        else if (_curlevel < 2 && _money >= _1to2)
        {
            LevelStatus.Money -= _1to2;
            UpLevel();
        }
        else if(_curlevel < 3 && _money >= _2to3)
        {
            LevelStatus.Money -= _2to3;
            UpLevel();
        }
        else
        {
            Debug.Log("---POOR---");
        }
        GameEvents.Instance.UpdateDisplay();
    }

    public void DowngradeButton()
    {
        if (_curlevel == 1)
        {
            Debug.Log("The turret is min level");
        }
        else if (_curlevel > 2)
        {
            LevelStatus.Money += _2to3;
            DownLevel();
        }
        else if (_curlevel > 1)
        {
            LevelStatus.Money += _1to2;
            DownLevel();
        }
        GameEvents.Instance.UpdateDisplay();
    }

    private void TurretSelected()
    {
        AudioManager.Instance.PlaySFX(turretSelected);
        _curGameObject = hit.transform.gameObject;
        _turretTag = _curGameObject.tag;
        UI.SetActive(true);
        
        if (_turretTag == "Shoot")
        {
            _turretLevel = _curGameObject.GetComponentInParent<TurretLevel>();
            _turretShoot = _curGameObject.GetComponentInParent<TurretShoot>();
            _turrentAoe = null;
            _turretThrowStone = null;
            _turretLazer = null;
        }
        else if(_turretTag == "AOE")
        {
            _turretLevel = _curGameObject.GetComponentInParent<TurretLevel>();
            _turrentAoe = _curGameObject.GetComponentInParent<TurrentAOE>();
            _turretShoot = null;
            _turretThrowStone = null;
            _turretLazer = null;
        }
        else if (_turretTag == "ThrowStone")
        {
            _turretLevel = _curGameObject.GetComponentInParent<TurretLevel>();
            _turretThrowStone = _curGameObject.GetComponentInParent<TurretThrowStone>();
            _turretShoot = null;
            _turrentAoe = null;
            _turretLazer = null;
        }
        else if(_turretTag == "Lazer")
        {
            _turretLevel = _curGameObject.GetComponent<TurretLevel>();
            _turretLazer = _curGameObject.GetComponent<TurretLazer>();
            _turretShoot = null;
            _turrentAoe = null;
            _turretThrowStone = null;
        }
        _curlevel = _turretLevel.currentLevel;

        // Debug.Log("物体信息:" + _curGameObject + "标签:" + _turretTag);
    }

    private void UpLevel()
    {
        AudioManager.Instance.PlaySFX(turretLevelUp);
        Debug.Log("UP");
        _turretLevel.currentLevel++;
        _curlevel++;
        if (_turretShoot != null)
        {
            _turretShoot.coolDown -= shoot_CD;
            _turretShoot.turnSpeed += shoot_turnSpeed;
        }
        if (_turrentAoe != null)
        {
            _turrentAoe.damage += aoe_Damage;
            _turrentAoe.coolDown -= aoe_CD;
            _turrentAoe.slowTime += aoe_SlowDownTime;
        }
        if (_turretThrowStone != null)
        {
            _turretThrowStone.coolDown -= stone_CD;
        }
        if (_turretLazer != null)
        {
            _turretLazer.burnDamage += lazer_BurnDamage;
            _turretLazer.burnDuration += lazer_BuranDuration;
            _turretLazer.burnWait -= lazer_CD;
        }
    }

    private void DownLevel()
    {
        AudioManager.Instance.PlaySFX(turretLevelDown);
        Debug.Log("DOWN");
        _turretLevel.currentLevel--;
        _curlevel--;
        if (_turretShoot != null)
        {
            _turretShoot.coolDown += shoot_CD;
            _turretShoot.turnSpeed -= shoot_turnSpeed;
        }
        if (_turrentAoe != null)
        {
            _turrentAoe.damage -= aoe_Damage;
            _turrentAoe.coolDown += aoe_CD;
            _turrentAoe.slowTime -= aoe_SlowDownTime;
        }
        if (_turretThrowStone != null)
        {
            _turretThrowStone.coolDown += stone_CD;
        }
        if (_turretLazer != null)
        {
            _turretLazer.burnDamage -= lazer_BurnDamage;
            _turretLazer.burnDuration -= lazer_BuranDuration;
            _turretLazer.burnWait += lazer_CD;
        }
    }

    public void CloseUpgradeMenu()
    {
        UI.SetActive(false);
    }
}