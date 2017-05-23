using System;
using System.Collections.Generic;

public class WeaponSystemData : BaseData
{
    public List<WeaponSysItemData> WeaponInfoLst = null;
    public int Money;
    public int Score;
    public int Energy;

    public WeaponSystemData()
    {
        WeaponInfoLst = new List<WeaponSysItemData>();
    }

}

