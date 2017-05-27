using System;
using System.Collections.Generic;
using UnityEngine;

//对象池
public class PoolMgr : Singleton<PoolMgr>
{
    private Dictionary<string, List<GameObject>> dictPool = null;
    private GameObject poolMgr;
    private GameObject PoolMgrObj
    {
        get
        {
            if (poolMgr == null)
            {
                poolMgr = new GameObject("PoolMgr");
            }
            return poolMgr;
        }
    }

    public override void init()
    {
        dictPool = new Dictionary<string, List<GameObject>>();
    }

    public void clearAll()
    {
        dictPool.Clear();
    }

    //public void saveObj(GameObject obj)
    //{
    //    string name = obj.name;
    //    if (!dictPool.ContainsKey(name))
    //    {
    //        dictPool.Add(name, new List<GameObject>());
    //    }
    //    obj.SetActive(false);
    //    dictPool[name].Add(obj);
    //}
    public void saveObj(GameObject obj, string name)
    {
        if (!dictPool.ContainsKey(name))
        {
            dictPool.Add(name, new List<GameObject>());
        }
        obj.SetActive(false);
        obj.transform.SetParent(PoolMgrObj.transform);
        dictPool[name].Add(obj);
    }

    public GameObject getObj(string name)
    {
        GameObject go = null;
        if (dictPool.ContainsKey(name))
        {
            if (dictPool[name].Count > 0)
            {
                go = dictPool[name][0];
                dictPool[name].RemoveAt(0);
                //go.SetActive(true);
                go.transform.SetParent(null);
            }
        }
        return go;
    }

}

