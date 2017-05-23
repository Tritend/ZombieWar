using System;
using System.Collections.Generic;
using UnityEngine;
using ChuMeng;

public class EntityInfo
{
    public int UID;
    public string Path;
    public EntityType Type;
    public EntitySonType SonType;
    public string Name;
    public int TempId;
    public int HP;
    public float NameHeight;
    public Vector3 SpawnPos;
    public List<int> Skills = new List<int>();
}

public class EntityMgr : Singleton<EntityMgr>
{
    //实体静态数据
    private Dictionary<int, EntityInfo> dictInfo = null;
    //实体管理列表
    private Dictionary<int, BaseEntity> dictEntityById = null;
    private Dictionary<EntityType, List<BaseEntity>> dictEntityByType = null;

    public override void init()
    {
        dictEntityById = new Dictionary<int, BaseEntity>();
        dictEntityByType = new Dictionary<EntityType, List<BaseEntity>>();
        dictInfo = new Dictionary<int, EntityInfo>();
        loadStaticData();
    }


    //创建entity
    public void createEntity(int tempId, int uid)
    {
        EntityInfo data = null;
        if (dictInfo.ContainsKey(tempId))
        {
            data = dictInfo[tempId];
            data.UID = uid;
        }
        if (data == null)
        {
            return;
        }
        onCreate(data);
    }

    private void onCreate(EntityInfo data)
    {
        ResMgr.Instance.load(data.Path, (obj) =>
        {
            GameObject go = obj as GameObject;
            BaseEntity be = go.GetComponent<BaseEntity>();
            if (be == null)
            {
                be = go.AddComponent(getType(data.Type)) as BaseEntity;
            }
            be.onCreate(data);
            this.addEntity(be);
        });
    }
    private Type getType(EntityType type)
    {
        Type t = null;
        switch (type)
        {
            case EntityType.staticActor:
                t = typeof(EntityCrystal);
                break;
            case EntityType.player:
                t = typeof(EntityMainPlayer);
                break;
            case EntityType.monster:
                t = typeof(EntityMonster);
                break;
        }
        return t;
    }


    //添加entity
    public void addEntity(BaseEntity entity)
    {
        if (!dictEntityById.ContainsKey(entity.UID))
        {
            dictEntityById.Add(entity.UID, entity);
        }
        if (!dictEntityByType.ContainsKey(entity.EType))
        {
            dictEntityByType.Add(entity.EType, new List<BaseEntity>());
        }
        if (!dictEntityByType[entity.EType].Contains(entity))
        {
            dictEntityByType[entity.EType].Add(entity);
        }
    }
    //移除entity
    public void removeEntity(BaseEntity entity)
    {
        if (entity == null || !dictEntityById.ContainsKey(entity.UID))
            return;

        if (dictEntityById.ContainsKey(entity.UID))
        {
            dictEntityById.Remove(entity.UID);
        }
        if (dictEntityByType.ContainsKey(entity.EType))
        {
            if (dictEntityByType[entity.EType].Contains(entity))
            {
                dictEntityByType[entity.EType].Remove(entity);
            }
        }
        EntityType type = entity.EType;
        GameObject.DestroyObject(entity.CacheObj);
        switch (type)
        {
            case EntityType.player:
                break;
            case EntityType.monster:
                Message Die_Monster_Entity = new Message(MsgCmd.Die_Monster_Entity, this);
                Die_Monster_Entity.Send();
                break;
            case EntityType.staticActor:
                Message Die_Crystal_Entity = new Message(MsgCmd.Die_Crystal_Entity, this);
                Die_Crystal_Entity.Send();
                break;
        }
    }
    //移除entity by type
    public void removeEntityByType(EntityType type)
    {
        if (!dictEntityByType.ContainsKey(type))
        {
            return;
        }
        List<BaseEntity> lst = new List<BaseEntity>(dictEntityByType[type]);
        for (int i = 0; i < lst.Count; i++)
        {
            GameObject.DestroyObject(lst[i].CacheObj);
        }
    }
    //移除所有实体
    public void removeAllEntity()
    {
        if (dictEntityById != null)
            foreach (var item in dictEntityById)
            {
                item.Value.onDispose();
            }
        dictEntityById.Clear();
        dictEntityByType.Clear();
    }

    //获取实体根据id
    public BaseEntity getEntityById(int uid)
    {
        if (dictEntityById.ContainsKey(uid))
        {
            return dictEntityById[uid];
        }
        return null;
    }
    //获取实体根据type
    public List<BaseEntity> getEntityByType(EntityType type)
    {
        if (dictEntityByType.ContainsKey(type))
        {
            return dictEntityByType[type];
        }
        return null;
    }

    //加载静态数据
    private void loadStaticData()
    {
        List<ModelConfigData> lst = GameData.ModelConfig;
        for (int i = 0; i < lst.Count; i++)
        {
            EntityInfo data = new EntityInfo();
            data.TempId = lst[i].tempId;
            data.Name = lst[i].name;
            data.Type = (EntityType)lst[i].type;
            data.SonType = (EntitySonType)lst[i].sonType;
            data.Path = lst[i].loadPath;
            data.HP = lst[i].hp;
            data.NameHeight = lst[i].nameHeight;
            //出生点
            string[] vec = lst[i].spawnPos.Split(',');
            Vector3 pos = new Vector3(int.Parse(vec[0]), int.Parse(vec[1]), int.Parse(vec[2]));
            data.SpawnPos = pos;
            //技能
            string[] skills = lst[i].skills.Split(',');
            for (int j = 0; j < skills.Length; j++)
            {
                if (data.Skills == null)
                {
                    data.Skills = new List<int>();
                }
                data.Skills.Add(int.Parse(skills[j]));
            }
            dictInfo.Add(lst[i].tempId, data);
        }
    }
}

