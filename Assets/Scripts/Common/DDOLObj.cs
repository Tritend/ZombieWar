using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DDOLObj : MonoBehaviour
{
    public static DDOLObj Instance;
    private List<BaseControl> controls = null;

    private void Awake()
    {
        MonoBehaviour.DontDestroyOnLoad(this.gameObject);
        this.gameObject.AddComponent<TimeMgr>();
        GameObject go = GameObject.Find("EventSystem");
        if (go == null)
        {
            go = new GameObject("EventSystem");
            go.AddComponent<EventSystem>();
            go.AddComponent<StandaloneInputModule>();
        }
        Instance = this;
    }

    private void Start()
    {
        //模拟服务器启动
        ServerTest.Instance.initServer();
        //初始化客户端所有control初始化(监听)
        controls = new List<BaseControl>();
        controls.Add(new WeaponSystemControl());
        controls.Add(new MainMeunControl());
        initControl();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SceneMgr.Instance.onLoadScene("CrossFire2", null, (progress) =>
            {
                DDOLCanvas.Instance.setFill(progress);
            }, true);
        }
    }

    //初始化control监听
    private void initControl()
    {
        for (int i = 0; i < controls.Count; i++)
        {
            controls[i].initListener();
            controls[i].initEnum();
        }
    }


}

