using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSysItemData : BaseData
{
    public WeaponType Type;
    public int CostMoney;
    public int BaseDamage;
    public int AddDamage;
    public string Path;
    //名字 描述\
    public string Name;
    public string Desc;
    public Vector3 Scale = new Vector3(100, 100, 1);
    public Vector3 Pos;
}

