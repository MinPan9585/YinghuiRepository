using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectInPool : MonoBehaviour, IPooledObject
{
    public abstract void OnObjectSpawn();

    public abstract void OnObjectDespawn();
}
