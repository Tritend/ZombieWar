using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSysUI : BaseUI
{
    private GameObject slot;
    private Text money;
    private Text score;
    private Text energy;
    private Dictionary<int, WeaponSysItem> dictItem = null;

    public override void resetUIInfo()
    {
        uiEnum = UIEnum.weaponSys;
        this.uiNode = UINode.main;
    }

    public override void onStart()
    {
        base.onStart();
        slot = this.cacheTrans.Find("itemContent/weaponSlot").gameObject;
        money = this.cacheTrans.Find("propertyContent/money/moneyText").GetComponent<Text>();
        score = this.cacheTrans.Find("propertyContent/score/scoreText").GetComponent<Text>();
        energy = this.cacheTrans.Find("propertyContent/energy/energyText").GetComponent<Text>();
        slot.SetActive(false);
        dictItem = new Dictionary<int, WeaponSysItem>();
        MessageCenter.Instance.addListener(MsgCmd.On_BB_Change_Value, onPropertyChanage);
    }

    private void onPropertyChanage(Message msg)
    {
        BType type = (BType)msg["type"];
        int val = (int)msg["val"];
        switch (type)
        {
            case BType.money:
                money.text = "金币: " + val;
                break;
            case BType.score:
                score.text = "分数: " + val;
                break;
            case BType.energy:
                energy.text = "能量: " + val;
                break;
        }
    }

    public override void refreshUI()
    {
        insSlot();
    }

    private void insSlot()
    {
        WeaponSystemData dt = this.data as WeaponSystemData;
        if (dt == null)
        {
            return;
        }
        money.text = "金币: " + dt.Money.ToString();
        score.text = "分数: " + dt.Score.ToString();
        energy.text = "能量: " + dt.Energy.ToString();

        List<WeaponSysItemData> lst = dt.WeaponInfoLst;
        if (lst != null && lst.Count > 0)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                if (!dictItem.ContainsKey(i))
                {
                    GameObject go = MonoBehaviour.Instantiate(slot, slot.transform.parent) as GameObject;
                    go.SetActive(true);
                    WeaponSysItem item = go.AddComponent<WeaponSysItem>();

                    //给实例化的item赋值Data 使它们各自有自己的武器系统信息
                    //item.Data = lst[i]; - -跟下面的setData(lst[i])一样作用

                    dictItem[i] = item;
                }
                dictItem[i].setData(lst[i]);
            }
        }
    }

    public override void onDispose()
    {
        MessageCenter.Instance.removeListener(MsgCmd.On_BB_Change_Value, onPropertyChanage);
    }
}

