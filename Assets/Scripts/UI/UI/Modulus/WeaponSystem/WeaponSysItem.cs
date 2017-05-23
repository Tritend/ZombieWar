using System;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine.UI;
using UnityEngine;

public class WeaponSysItem : BaseUI
{
    private Text weaponName;
    private Text weaponCost;
    private GameObject weapon;
    public int CostMoney;
    private WeaponSysItemData dt;
    //武器相关信息，实例化的时候赋值过来的 --------ps：是Data不是小写
    //public WeaponSysItemData Data;

    public override void resetUIInfo()
    {
        this.uiEnum = UIEnum.none;
        this.uiNode = UINode.none;
    }

    public override void onStart()
    {
        base.onStart();
        weaponName = this.cacheTrans.Find("weaponBtn/weaponName").GetComponent<Text>();
        weaponCost = this.cacheTrans.Find("weaponCost").GetComponent<Text>();
        this.cacheObj.AddComponent<UIEventTrigger>().setClickHandler(onBtnClick);
        //进入button，打开Tips
        this.cacheObj.GetComponent<UIEventTrigger>().setEnterHandler(OnOpenTips);
        dt=this.data as WeaponSysItemData;
        CostMoney = dt.CostMoney;
    }

    private void OnOpenTips()
    {
        //WeaponSysItemData dt = this.data as WeaponSysItemData;
        dt.Pos = this.cacheTrans.position;
        UIMgr.Instance.openUI(UIEnum.weaponSysTips, dt);
    }

    private void onBtnClick()
    {
        WeaponSysItemData dt = this.data as WeaponSysItemData;
        if (dt != null)
        {
            Message msg = new Message(MsgCmd.On_Change_Weapon, this);
            msg["type"] = dt.Type;
            msg.Send();
        }
    }

    public override void refreshUI()
    {
        //WeaponSysItemData dt = this.data as WeaponSysItemData;
        if (dt != null)
        {
            weaponName.text = dt.Name.ToString();
            weaponCost.text = "售价: " + dt.CostMoney.ToString();
            if (weapon == null)
            {
                //演示在control做处理
                string path = dt.Path;
                ResMgr.Instance.load(path, (obj) =>
                {
                    weapon = obj as GameObject;
                    weapon.transform.SetParent(this.cacheTrans);
                    weapon.transform.position = new Vector3(this.cacheTrans.position.x, this.cacheTrans.position.y - 1.5f, this.cacheTrans.position.z);
                    weapon.transform.localScale = dt.Scale;
                    weapon.transform.localEulerAngles = new Vector3(0, 0, 0);
                });
            }
        }
    }
}

