using System;
using System.Collections.Generic;
using ChuMeng;
using UnityEngine;

public class EffectInfo
{
    public Transform parent;
    public Vector3 initPos;
    public Vector3 initAngle = new Vector3(0, 0, 0);
    public Vector3 initScale = new Vector3(1, 1, 1);
    public EffectInfo(Vector3 pos)
    {
        this.initPos = pos;
    }

    public EffectInfo(Vector3 pos, Transform parent)
    {
        this.initPos = pos;
        this.parent = parent;
    }
    public EffectInfo(Vector3 pos, Vector3 angle, Transform parent)
    {
        this.initPos = pos;
        this.parent = parent;
        this.initAngle = angle;
    }
    public EffectInfo(Vector3 pos, Vector3 angle, Vector3 scale, Transform parent)
    {
        this.initPos = pos;
        this.parent = parent;
        this.initScale = scale;
        this.initAngle = angle;
    }
}

public class EffectMgr : Singleton<EffectMgr>
{
    private Dictionary<int, EffectConfigData> dictEff = null;

    public override void init()
    {
        base.init();
        dictEff = new Dictionary<int, EffectConfigData>();
        List<EffectConfigData> lst = GameData.EffectConfig;
        for (int i = 0; i < lst.Count; i++)
        {
            dictEff.Add(lst[i].tempId, lst[i]);
        }
    }


    public void createEffect(int effId)
    {
        createEffect(effId, null);
    }

    public void createEffect(int effId, EffectInfo info, bool isUseParent = true)
    {
        if (dictEff.ContainsKey(effId))
        {
            EffectConfigData config = dictEff[effId];
            ResMgr.Instance.load(config.path, (obj) =>
            {
                GameObject go = obj as GameObject;
                if (isUseParent)
                    go.transform.SetParent(info.parent);
                go.transform.position = info.initPos;
                go.transform.localEulerAngles = info.initAngle;
                go.transform.localScale = info.initScale;
            });
        }
    }

}