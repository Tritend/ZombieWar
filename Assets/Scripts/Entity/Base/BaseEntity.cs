using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{
    //缓存gameObj trans
    private GameObject cacheObj;
    public GameObject CacheObj
    {
        get
        {
            if (cacheObj == null)
            {
                cacheObj = this.gameObject;
            }
            return cacheObj;
        }
    }
    private Transform cacheTrans;
    public Transform CacheTrans
    {
        get
        {
            if (cacheTrans == null)
            {
                cacheTrans = this.transform;
            }
            return cacheTrans;
        }
    }
    //唯一ID
    private int uid;
    public int UID
    {
        get
        {
            return this.uid;
        }
        set
        {
            this.uid = value;
        }
    }
    //实体类型
    private EntityType eType;
    public EntityType EType
    {
        get
        {
            return this.eType;
        }
        set
        {
            this.eType = value;
        }
    }
    //子类型
    private EntitySonType sonType;
    public EntitySonType SonType
    {
        get
        {
            return sonType;
        }
        set
        {
            this.sonType = value;
        }
    }

    protected BillBoardWidget BillBoard;
    protected float OrgHP;
    private float hp;
    public float HP
    {
        get
        {
            return this.hp;
        }
        protected set
        {
            this.hp = value;
            if (this.BillBoard != null)
                this.BillBoard.setFloatByType(PartType.bloodPart, (this.hp / this.OrgHP) < 0 ? 0 : this.hp / this.OrgHP);
        }
    }
    public string Name;
    protected List<int> Skills = null;
    protected EntityInfo info;
    protected BlackBoard BB;

    private void Awake()
    {
        BB = new BlackBoard();
        onAwake();
    }

    public virtual void onAwake()
    {

    }

    private void Start()
    {
        onStart();
    }
    public virtual void onStart()
    {

    }

    private void Update()
    {
        onUpdate();
    }
    public virtual void onUpdate()
    {

    }

    //当实体创建
    public virtual void onCreate(EntityInfo data)
    {
        this.info = data;
        this.EType = data.Type;
        this.HP = data.HP;
        this.OrgHP = data.HP;
        this.Name = data.Name;
        this.UID = data.UID;
        this.SonType = data.SonType;
        this.CacheTrans.position = data.SpawnPos;
        this.Skills = new List<int>(data.Skills);
    }

    //实体受伤
    public virtual bool isCanAttack()
    {
        return true;
    }
    public virtual void onDamage(float damage)
    {

    }

    public virtual void onChangeValue(Message msg)
    {
        BType type = (BType)msg["type"];
        int val = (int)msg["val"];
        this.BB.onChangeValue(type, val);
    }

    public virtual int getValue(BType type)
    {
        return this.BB.getValue(type);
    }

    //模型颜色改变
    private Renderer render;
    private Tweener tweener;
    public virtual void onChangeColor()
    {
        if (tweener == null)
        {
            tweener = DOTween.To((progress) =>
            {
                //0,0,0,1 -> 1,0,0,1
                if (render == null)
                {
                    render = this.CacheObj.GetComponentInChildren<Renderer>();
                }
                if (render != null)
                {
                    render.material.SetColor("_EmissionColor", new Color(progress, 0, 0));
                }
            }, 0, 1, 0.5f).OnComplete(() =>
            {
                DOTween.To((progress2) =>
                {
                    if (render == null)
                    {
                        render = this.CacheObj.GetComponentInChildren<Renderer>();
                    }
                    if (render != null)
                    {
                        render.material.SetColor("_EmissionColor", new Color(progress2, 0, 0));
                    }
                }, 1, 0, 0.5f).OnComplete(() =>
                {
                    tweener.Kill(false);
                    tweener = null;
                });
            });
        }
    }

    private void OnDestroy()
    {
        onDispose();
    }
    public virtual void onDispose()
    {

    }


}

