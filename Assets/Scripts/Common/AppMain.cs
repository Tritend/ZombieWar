using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppMain : MonoBehaviour
{
    private void Awake()
    {
        init();
    }


    private void Start()
    {
        SceneMgr.Instance.onLoadScene("GameStart", null, (progress) =>
        {
            DDOLCanvas.Instance.setFill(progress);
        }, true);
    }


    private void init()
    {
        GameObject go = GameObject.Find("DDOLObj");
        if (go == null)
        {
            go = new GameObject("DDOLObj");
            go.AddComponent<DDOLObj>();
        }
        GameObject eventSys = GameObject.Find("EventSystem");
        if (eventSys != null)
        {
            MonoBehaviour.DontDestroyOnLoad(eventSys);
        }
        GameObject load = GameObject.Find("LoadCanvas");
        if (load == null)
        {
            UnityEngine.Object canvas = Resources.Load("UI/LoadCanvas");
            load = MonoBehaviour.Instantiate(canvas) as GameObject;
            if (load.GetComponent<DDOLCanvas>() == null)
            {
                load.AddComponent<DDOLCanvas>();
            }
        }
    }

}
