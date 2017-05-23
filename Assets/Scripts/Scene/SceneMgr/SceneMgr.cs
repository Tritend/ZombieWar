using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using ChuMeng;

public class SceneInfo
{
    public int LevelIndex;
    public string LevelName;
    //AI生成时间
    public int AISpawnTimer = 0;
    //AI生成波数
    public int AIWave = 0;
    //AI生成列表
    public List<int> LstAI = new List<int>();

    //水晶生成时间
    public int CrystalTime = 0;
    //水晶生成列表
    public List<int> LstCrystal = new List<int>();
}

public class SceneMgr : Singleton<SceneMgr>
{
    private Dictionary<string, SceneInfo> dictInfo = null;

    public override void init()
    {
        dictInfo = new Dictionary<string, SceneInfo>();
        initConfig();
    }

    public void onLoadScene(string name, bool isAsync = false)
    {
        onLoadScene(name, null, isAsync);
    }
    public void onLoadScene(string name, Action<string> loaded, bool isAsync = false)
    {
        onLoadScene(name, loaded, null, isAsync);
    }
    public void onLoadScene(string name, Action<string> loaded, Action<float> progress, bool isAsync = false)
    {
        DDOLObj.Instance.StartCoroutine(realLoad(name, loaded, progress, isAsync));
    }

    IEnumerator realLoad(string name, Action<string> loaded, Action<float> progress, bool isAsync = false)
    {
        TimeMgr.Instance.removeALLTimerHanlder();
        EntityMgr.Instance.removeAllEntity();
        UIMgr.Instance.onClear();
        yield return null;
        if (!isAsync)
        {
            SceneManager.LoadScene(name);
            onSceneLoadFinished(name);
            yield break;
        }
        else
        {
            AsyncOperation info = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
            DDOLCanvas.Instance.showLoadCnavas();
            while (!info.isDone)
            {
                if (progress != null)
                {
                    progress(info.progress);
                }
                yield return null;
            }
            if (info.isDone)
            {
                if (loaded != null)
                {
                    loaded(name);
                    Message msg = new Message(MsgCmd.On_Scene_Load_Finished, this);
                    msg.Send();
                }
                DDOLCanvas.Instance.hideLoadCnavas();
                onSceneLoadFinished(name);
                yield break;
            }
        }
    }

    private void onSceneLoadFinished(string name)
    {
        GameObject go = GameObject.Find("SceneControl");
        if (go == null)
        {
            go = new GameObject("SceneControl");
            BaseSceneControl control = go.AddComponent(getTypeOfScene(name)) as BaseSceneControl;
            if (control != null)
                control.setData(getSceneInfo(name));
        }
    }

    //根据name 获取type
    private Type getTypeOfScene(string name)
    {
        Type t = null;
        switch (name)
        {
            case "GameStart":
                t = typeof(SceneStartGameControl);
                break;
            default:
                t = typeof(SceneNormalControl);
                break;
        }
        return t;
    }

    //关卡配置表相关
    private SceneInfo getSceneInfo(string levelName)
    {
        SceneInfo levelInfo = null;
        if (dictInfo.ContainsKey(levelName))
            levelInfo = dictInfo[levelName];
        return levelInfo;
    }
    private void initConfig()
    {
        List<LevelDesignData> lst = new List<LevelDesignData>(GameData.LevelDesign);
        for (int i = 0; i < lst.Count; i++)
        {
            SceneInfo dt = new SceneInfo();
            dt.LevelName = lst[i].levelName;
            dt.LevelIndex = lst[i].diff;
            dt.AISpawnTimer = lst[i].AISpawnTime;
            dt.AIWave = lst[i].AIWave;
            string[] aiLst = lst[i].monsterLst.Split(',');
            for (int j = 0; j < aiLst.Length; j++)
            {
                dt.LstAI.Add(int.Parse(aiLst[j]));
            }

            dt.CrystalTime = lst[i].crystalSpawnTime;
            string[] cryLst = lst[i].crystalLst.Split(',');
            for (int j = 0; j < cryLst.Length; j++)
            {
                dt.LstCrystal.Add(int.Parse(cryLst[j]));
            }
            dictInfo.Add(dt.LevelName, dt);
        }
    }

}

