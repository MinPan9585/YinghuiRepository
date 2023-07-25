using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBurn 
{
    void Burn(float burnWait, float burnCount, float timePassed, int lazerDamage, int burnDamage);
}
