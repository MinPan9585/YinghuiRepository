using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelStatus //: MonoBehaviour 
{
    private static int _lives = 100;
    public static int Lives
    {
        get { return _lives; }

        set 
        {
            _lives = value;
            if(_lives <= 0)
            {
                _lives = 0;
                Debug.LogWarning("Die!!!");
                GameEvents.Instance.LoseTheGame();
            }
        }
    }

    private static int _money = 400;
    public static int Money
    {
        get { return _money; }

        set 
        { 
            _money = value;
            if (_money < 0)
            {
                _money = 0;
                Debug.LogWarning("Not enough money");
            }
        }
    }

    //#region Singleton
    //private static LevelStatus _levelStatus;
    //public static LevelStatus Instance
    //{
    //    get
    //    {
    //        if (_levelStatus == null)
    //        {
    //            GameObject.Find("GameMaster").AddComponent<LevelStatus>();
    //        }
    //        return _levelStatus;
    //    }
    //}
    //#endregion

    //private void Awake()
    //{
    //    if (_levelStatus != null && _levelStatus != this)
    //    {
    //        Destroy(this.gameObject);
    //    }
    //    else
    //    {
    //        _levelStatus = this;
    //    }
    //}

    

}

