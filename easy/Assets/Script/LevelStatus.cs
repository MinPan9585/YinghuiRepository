using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatus : MonoBehaviour
{
    private static int _money;
    public int Money
    {
        get { return _money; }
        set { _money = value; }
    }

    private static int _lives;
    public int Lives;

    public static int Rounds;
    
    //Singleton
    private static LevelStatus _instance;
    public static LevelStatus Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    //Singleton end

}

