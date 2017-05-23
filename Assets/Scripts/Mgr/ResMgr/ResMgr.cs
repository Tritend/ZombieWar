using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AssetInfo
{
    public string Path;
    public UnityEngine.Object Obj;
    public bool isDestroy = false;

    //即时加载
    public UnityEngine.Object loadImm()
    {
        this.Obj = this.Obj != null ? this.Obj : ResMgr.Instance.resLoad(this.Path);
        return ResMgr.Instance.insObj(this.Obj);
    }
    //协程即时加载
    public IEnumerator loadCoroutine(Action<UnityEngine.Object> loaded)
    {
        this.Obj = this.Obj != null ? this.Obj : ResMgr.Instance.resLoad(this.Path);
        yield return null;
        if (loaded != null && this.Obj != null)
        {
            loaded(ResMgr.Instance.insObj(this.Obj));
        }
    }
    //协程异步加载
    public IEnumerator loadAsync(Action<UnityEngine.Object> loaded)
    {
        return loadAsync(loaded, null);
    }
    public IEnumerator loadAsync(Action<UnityEngine.Object> loaded, Action<float> progress)
    {
        if (this.Obj != null)
        {
            if (loaded != null)
            {
                loaded(ResMgr.Instance.insObj(this.Obj));
            }
            yield break;
        }
        ResourceRequest req = Resources.LoadAsync(this.Path);
        while (!req.isDone)
        {
            if (progress != null)
            {
                progress(req.progress);
            }
            yield return req;
        }
        this.Obj = req.asset;
        if (loaded != null)
        {
            loaded(ResMgr.Instance.insObj(this.Obj));
        }
        yield break;
    }
}

public class ResMgr : Singleton<ResMgr>
{
    public Dictionary<string, AssetInfo> dictAsset = null;
    public override void init()
    {
        dictAsset = new Dictionary<string, AssetInfo>();
    }

    public UnityEngine.Object load(string path)
    {
        AssetInfo info = null;
        if (!dictAsset.TryGetValue(path, out info))
        {
            info = new AssetInfo();
            info.Path = path;
            dictAsset.Add(path, info);
        }
        return info.loadImm();
    }
    public void load(string path, Action<UnityEngine.Object> loaded)
    {
        load(path, loaded, null);
    }
    public void load(string path, Action<UnityEngine.Object> loaded, Action<float> progress, LoadType type = LoadType.coroutine)
    {
        if (string.IsNullOrEmpty(path))
        {
            return;
        }
        AssetInfo info = null;
        if (!dictAsset.TryGetValue(path, out info))
        {
            info = new AssetInfo();
            info.Path = path;
            dictAsset.Add(path, info);
        }

        switch (type)
        {
            case LoadType.coroutine:
                DDOLObj.Instance.StartCoroutine(info.loadCoroutine(loaded));
                break;
            case LoadType.async:
                DDOLObj.Instance.StartCoroutine(info.loadAsync(loaded, progress));
                break;
            default:
                DDOLObj.Instance.StartCoroutine(info.loadCoroutine(loaded));
                break;
        }
    }

    public UnityEngine.Object resLoad(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            Debug.Log("<color=red>ERROR：路径为空</color>");
            return null;
        }
        UnityEngine.Object obj = null;
        obj = Resources.Load(path);
        if (obj == null)
        {
            Debug.Log("<color=red>ERROR：资源是否存在或资源路径错误</color>");
        }
        return obj;
    }
    public UnityEngine.Object insObj(UnityEngine.Object obj)
    {
        if (obj == null)
        {
            return null;
        }
        return MonoBehaviour.Instantiate(obj);
    }


    //加载sprite
    public Sprite loadSprite(string name)
    {
        string path = "Textures/" + name;
        return Resources.Load<Sprite>(path);
    }

}
