using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMeunItemUI : BaseUI
{
    private Text levelName;
    public int Level;

    public override void resetUIInfo()
    {
        this.uiEnum = UIEnum.none;
        this.uiNode = UINode.none;
    }

    public override void onStart()
    {
        levelName = this.cacheTrans.Find("LevelText").GetComponent<Text>();
        UIEventTrigger listener = this.cacheObj.AddComponent<UIEventTrigger>();
        listener.setClickHandler(onloadScene);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)&& Level==1)
        {
            onloadScene();
        }
    }
    public override void refreshUI()
    {
        MainMeunItemData dt = this.data as MainMeunItemData;
        if (dt != null)
        {
            levelName.text = dt.Name;
        }
    }

    private void onloadScene()
    {
        MainMeunItemData dt = this.data as MainMeunItemData;
        if (dt != null)
            SceneMgr.Instance.onLoadScene(dt.LevelName, null, (progress) =>
            {
                DDOLCanvas.Instance.setFill(progress);
            }, true);
    }

}

