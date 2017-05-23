using System;
using System.Collections.Generic;
using UnityEngine;
using ChuMeng;

public class WeaponInfo
{
    public WeaponType Type;
    public string LeftPath;
    public string RightPath;
    public int CostMoney;
    public int BaseDamage;
    public int AddDamage;
    public string Name;
    public string Desc;
}

public class WeaponFactory : Singleton<WeaponFactory>
{
    private Dictionary<WeaponType, WeaponInfo> dictInfo = null;

    public override void init()
    {
        dictInfo = new Dictionary<WeaponType, WeaponInfo>();
        initConfig();
    }

    //创建武器
    public void createWeapon(WeaponType wType, BaseEntity entity)
    {
        if (!dictInfo.ContainsKey(wType))
        {
            Debug.Log("<color=red>配置表未配置此武器</color>");
            return;
        }
        WeaponInfo info = dictInfo[wType];
        //弓箭做特殊处理
        if (wType == WeaponType.bow)
        {
            string leftPath = info.LeftPath;
            string rightPath = info.RightPath;
            loadWeapon(leftPath, typeof(WeaponBow), entity, info);
            loadWeapon(rightPath, typeof(WeaponArrow), entity, info);
        }
        else
        {
            string rightPath = info.RightPath;
            loadWeapon(rightPath, getWeaponType(wType), entity, info);
        }
    }
    //加载武器资源
    private void loadWeapon(string path, Type t, BaseEntity entity, WeaponInfo info)
    {
        ResMgr.Instance.load(path, (obj) =>
        {
            GameObject go = obj as GameObject;
            BaseWeapon bw = go.GetComponent<BaseWeapon>();
            if (bw == null)
            {
                bw = go.AddComponent(t) as BaseWeapon;
            }
            bw.setAgent(entity);
            bw.setInfo(info);
        });
    }
    //武器绑定脚本
    private Type getWeaponType(WeaponType wType)
    {
        Type t = null;
        switch (wType)
        {
            case WeaponType.gun:
                t = typeof(WeaponGun);
                break;
            case WeaponType.AK47:
                t = typeof(WeaponAK47);
                break;
            case WeaponType.shotGun:
                t = typeof(WeaponShotGun);
                break;
            default:
                Debug.Log("<color=red>现在没有这把武器</color>");
                break;
        }
        return t;
    }
    //武器静态数据
    private void initConfig()
    {
        List<SupplyConfigData> lst = GameData.SupplyConfig;
        for (int i = 0; i < lst.Count; i++)
        {
            WeaponInfo data = new WeaponInfo();
            data.Type = (WeaponType)lst[i].tempId;
            data.RightPath = lst[i].rightPath;
            data.LeftPath = lst[i].leftPath;
            data.CostMoney = lst[i].costMoney;
            data.BaseDamage = lst[i].baseDamage;
            data.AddDamage = lst[i].addDamage;
            data.Name = lst[i].name;
            data.Desc = lst[i].desc;
            dictInfo.Add(data.Type, data);
        }
    }
    //接口 获取所有武器信息
    public WeaponInfo getWeaponInfo(WeaponType wType)
    {
        if (dictInfo.ContainsKey(wType))
        {
            return dictInfo[wType];
        }
        return null;
    }
    public List<WeaponInfo> getWeaponInfo()
    {
        return new List<WeaponInfo>(dictInfo.Values);
    }
}

