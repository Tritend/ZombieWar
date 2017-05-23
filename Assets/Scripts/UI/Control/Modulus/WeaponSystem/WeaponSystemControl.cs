using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystemControl : BaseControl
{
    private List<WeaponSysItemData> lstInfo = null;

    public override void initEnum()
    {
        this.uiEnum = UIEnum.weaponSys;
        lstInfo = new List<WeaponSysItemData>();
    }

    public override void initListener()
    {
        MessageCenter.Instance.addListener(MsgCmd.Open_WeaponSystem_UI, onOpenUI);
    }

    private void onOpenUI(Message msg)
    {
        if (UIMgr.Instance.isOpen(this.uiEnum))
        {
            UIMgr.Instance.closeUI(this.uiEnum);
            return;
        }

        if (lstInfo != null && lstInfo.Count <= 0)
        {
            List<WeaponInfo> lst = WeaponFactory.Instance.getWeaponInfo();
            for (int i = 0; i < lst.Count; i++)
            {
                WeaponSysItemData dt = new WeaponSysItemData();
                dt.Type = lst[i].Type;
                dt.CostMoney = lst[i].CostMoney;
                dt.BaseDamage = lst[i].BaseDamage;
                dt.AddDamage = lst[i].AddDamage;
                dt.Path = dt.Type == WeaponType.bow ? lst[i].LeftPath : lst[i].RightPath;
                dt.Name = lst[i].Name;
                dt.Desc = lst[i].Desc;
                dt.Scale = dt.Type == WeaponType.bow ? new Vector3(10, 10, 0.1f) : new Vector3(100, 100, 1);
                lstInfo.Add(dt);
            }
        }
        WeaponSystemData data = new WeaponSystemData();
        data.WeaponInfoLst = lstInfo;

        //BaseEntity player = EntityMgr.Instance.getEntityById(1008611);
        List<BaseEntity> playerLst = EntityMgr.Instance.getEntityByType(EntityType.player);
        BaseEntity player = playerLst != null && playerLst.Count > 0 ? playerLst[0] : null;
        if (player != null)
        {
            data.Money = player.getValue(BType.money);
            data.Score = player.getValue(BType.score);
            data.Energy = player.getValue(BType.energy);
        }
        //canvas的位置旋转
        Vector3 pos = (Vector3)msg["Pos"];
        Quaternion rot = (Quaternion)msg["Rot"];
        UIMgr.Instance.resetCanvasPos(pos, rot);
        this.updateUI(data);
    }

}

