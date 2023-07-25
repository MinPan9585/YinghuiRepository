using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public string ResourceDir = "";
    
    Dictionary<string, SubPool> m_pools = new Dictionary<string, SubPool>();

    #region Singleton
    public static PoolManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    //Get an object
    public GameObject SpawnFromSubPool(string name, Transform trans)
    {
        SubPool pool = null;
        if (!m_pools.ContainsKey(name))
        {
            RegisterNewPool(name, trans);
        }
        pool = m_pools[name];

        return pool.Spawn();
    }

    //Despawn
    public void DespawnFromSubPool(GameObject go)
    {
        SubPool pool = null;
        foreach (var p in m_pools.Values)
        {
            if (p.Contain(go))
            {
                pool = p;
                break;
            }
        }

        pool.Despawn(go);
    }
    public void DespawnFromSubPool(GameObject go, int id)
    {
        SubPool pool = null;
        foreach (var p in m_pools.Values)
        {
            if (p.Contain(go))
            {
                pool = p;
                break;
            }
        }

        pool.Despawn(go, id);
    }

    //Clear all Subpools
    public void ClearAllSubPool()
    {
        foreach (var p in m_pools.Values)
        {
            p.ClearAll();
        }
    }

    public int GetPooledIndex(GameObject go)
    {
        SubPool pool = null;
        foreach (var p in m_pools.Values)
        {
            if (p.Contain(go))
            {
                pool = p;
                break;
            }
        }

        return pool.GetSubPoolIndex(go);
    }

    void RegisterNewPool(string names, Transform trans)
    {
        string path = ResourceDir + "/" + names;
        GameObject go = Resources.Load<GameObject>(path);
        SubPool pool = new SubPool(trans, go);
        m_pools.Add(pool.Name, pool);
    }
}
