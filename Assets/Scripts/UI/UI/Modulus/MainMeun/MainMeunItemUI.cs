using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMeunItemUI : BaseUI
{
    private Text levelName;

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
        {
            SceneMgr.Instance.onLoadScene(dt.LevelName, null, (progress) =>
            {
                DDOLCanvas.Instance.setFill(progress);
            }, true);
            SoundMgr.Instance.playAudioBg("bgMusic");
        }

    }

}

