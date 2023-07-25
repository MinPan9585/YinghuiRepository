using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPool
{
    List<GameObject> m_objects = new List<GameObject>();

    GameObject m_prefab;

    public string Name
    {
        get { return m_prefab.name;}
    }

    Transform m_parent;

    public SubPool(Transform parent, GameObject go)
    {
        m_prefab = go;
        m_parent = parent;
    }

    public GameObject Spawn()
    {
        GameObject go = null;

        foreach (var obj in m_objects)
        {
            if (!obj.activeSelf)
            {
                go = obj;
            }
        }

        if(go == null)
        {
            go = GameObject.Instantiate<GameObject>(m_prefab);
            go.transform.parent = m_parent;
            m_objects.Add(go);
        }

        go.SetActive(true);
        go.SendMessage("OnObjectSpawn", SendMessageOptions.DontRequireReceiver);

        //option two, functional, but need to change this
        
        PooledObjectAttachment attachment = go.GetComponent<PooledObjectAttachment>();
        if (attachment == null) 
            attachment = go.AddComponent<PooledObjectAttachment>();
        attachment.SetPool(this);

        return go;
    }

    public void Despawn(GameObject go)
    {
        if (Contain(go))
        {
            //go.SendMessage("OnObjectDespawn", SendMessageOptions.DontRequireReceiver);
            go.SetActive(false);
        }
    }
    public void Despawn(GameObject go, int id)
    {
        if (Contain(go))
        {
            m_objects[id].SetActive(false);
        }
    }

    public void ClearAll()
    {
        foreach (var obj in m_objects)
        {
            if (obj.activeSelf)
            {
                Despawn(obj);
            }
        }
    }

    

    //Judge if in the list or not
    public bool Contain(GameObject go)
    {
        return m_objects.Contains(go);
    }

    public int GetSubPoolIndex(GameObject go)
    {
        return m_objects.IndexOf(go);
    }
}
