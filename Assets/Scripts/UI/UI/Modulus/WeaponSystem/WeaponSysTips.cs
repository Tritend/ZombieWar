using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSysTips : BaseUI
{
    private Text weaponName;
    private Text baseDamage;
    private Text addDamage;
    private Text costMoney;
    private Text descText;

    public override void resetUIInfo()
    {
        this.uiEnum = UIEnum.weaponSysTips;
        this.uiNode = UINode.pop;
    }

    public override void onStart()
    {
        weaponName = this.cacheTrans.Find("NameText").GetComponent<Text>();
        baseDamage = this.cacheTrans.Find("property/BaseTextDesc/BaseText").GetComponent<Text>();
        addDamage = this.cacheTrans.Find("property/AddTextDesc/AddText").GetComponent<Text>();
        costMoney = this.cacheTrans.Find("property/CostTextDesc/CostText").GetComponent<Text>();
        descText = this.cacheTrans.Find("DescTextDesc/DescText").GetComponent<Text>();
    }

    public override void refreshUI()
    {
        WeaponSysItemData dt = this.data as WeaponSysItemData;
        if (dt != null)
        {
            this.cacheTrans.position = dt.TipsPos;
            weaponName.text = dt.Name;
            baseDamage.text = dt.BaseDamage.ToString();
            addDamage.text = dt.AddDamage.ToString();
            costMoney.text = dt.CostMoney.ToString();
            descText.text = dt.Desc;
        }
    }

}

