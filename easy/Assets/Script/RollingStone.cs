﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingStone : MonoBehaviour, IPooledObject
{
    public int stoneDamage = 20;
    #region Pool
    //using pool start
    //Must have, even left blank. Also, put everything in Start() function here
    public void OnObjectSpawn()
    {
        Invoke(nameof(DestroyStone), 5f);
        RestoreValues(); //注释:这是一个例子,将原本在Start()内的放到这里
    }
    //Must have, even left blank.
    public void OnObjectDespawn()//暂时没想好要不要删掉，就先留着了
    {
    }
    //specifically for restoring some values, like health
    public void RestoreValues()//示例，这是敌人的，恢复敌人的血量
    {
        
    }
    private void OnDisable()//用这个来代替OnObjectDespawn()
    {
        //可以是重置位置，停止计时器之类的
    }
    //using pool end
    #endregion

    private void DestroyStone()
    {
        GetComponent<PooledObjectAttachment>().PutBackToPool();
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.transform.CompareTag("Enemy"))
    //    {
    //        EnemyNav0519 e = collision.transform.GetComponent<EnemyNav0519>();
    //        e.TakeDamage(stoneDamage);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyNav0519 e = other.GetComponent<EnemyNav0519>();
            e.TakeDamage(stoneDamage);
        }
    }
}