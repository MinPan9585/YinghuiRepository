using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileTurret;
    public TurretBlueprint laserTurret;
    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectStandardTurret()
    {
        //Debug.Log("Standard Turret Purchased");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileTurret()
    {
        //Debug.Log("Another Turret Purchased");
        buildManager.SelectTurretToBuild(missileTurret);
    }

    public void SelectLaserTurret()
    {
        buildManager.SelectTurretToBuild(laserTurret);
    }
}
