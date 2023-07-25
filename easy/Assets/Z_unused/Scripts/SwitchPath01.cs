using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPath01 : MonoBehaviour
{
    public GameObject rotationSwitch;
    public bool isLeft = true;

    private void OnMouseDown()
    {
        GameObject[] enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy");
        List<Enemy> enemiesChanged = new List<Enemy>();
        foreach (var enemyObject in enemiesInScene)
        {
            if(enemyObject.GetComponent<Enemy>()!= null)
            {
                Debug.Log(enemyObject.GetComponent<Enemy>().wavepointIndex);
                if (enemyObject.GetComponent<Enemy>().wavepointIndex <= 1)
                {
                    enemiesChanged.Add(enemyObject.GetComponent<Enemy>());
                }
            }
            
        }
        isLeft = !isLeft;
        if (isLeft)
        {
            rotationSwitch.transform.rotation = Quaternion.Euler(0, 90, 0);
            foreach (var enemy in enemiesChanged)
            {
                enemy.currentWay = enemy.wp0508A;
            }
        }
        else
        {
            rotationSwitch.transform.rotation = Quaternion.Euler(0, 0, 0);
            foreach (var enemy in enemiesChanged)
            {
                enemy.currentWay = enemy.wp0508B;
            }
        }
        
    }
}