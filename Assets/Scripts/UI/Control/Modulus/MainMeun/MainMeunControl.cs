using System;
using System.Collections.Generic;
using ChuMeng;
using UnityEngine;

public class MainMeunControl : BaseControl
{
    private List<MainMeunItemData> infoLst = null;

    public override void initEnum()
    {
        this.uiEnum = UIEnum.mainMeun;
    }

    public override void initListener()
    {
        MessageCenter.Instance.addListener(MsgCmd.Open_Main_Meun_UI, onOpenUI);
    }

    private void onOpenUI(Message msg)
    {
        if (infoLst == null || infoLst.Count <= 0)
        {
            infoLst = new List<MainMeunItemData>();
            List<LevelDesignData> lst = new List<LevelDesignData>(GameData.LevelDesign);
            for (int i = 0; i < lst.Count; i++)
            {
                MainMeunItemData dt = new MainMeunItemData();
                dt.Name = lst[i].name;
                dt.LevelName = lst[i].levelName;
                infoLst.Add(dt);
            }
        }

        MainMeunData data = new MainMeunData();
        data.ItemLst = infoLst;

        //canvas的位置旋转
        //Vector3 pos = (Vector3)msg["Pos"];
        //Quaternion rot = (Quaternion)msg["Rot"];
        //UIMgr.Instance.resetCanvasPos(pos, rot);

        this.updateUI(data);
    }
}

