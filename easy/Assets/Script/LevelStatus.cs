using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelStatus //: MonoBehaviour 
{
    private static int _lives = 20;
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

    public static int TotalRound = 0;
    private static int _round = 0;
    public static int Round
    {
        get { return _round; }

        set
        {
            _round = value;
            if (_round > TotalRound)
            {
                _round = TotalRound;
                Debug.LogWarning("Last round is out");
            }
        }
    }


}
