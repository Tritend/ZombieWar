using System;
using System.Collections.Generic;
using ChuMeng;

public class SkillConfig : Singleton<SkillConfig>
{
    private Dictionary<int, SkillConfigData> dict = null;

    public override void init()
    {
        dict = new Dictionary<int, SkillConfigData>();
        List<SkillConfigData> conf = new List<SkillConfigData>(GameData.SkillConfig);
        for (int i = 0; i < conf.Count; i++)
        {
            dict.Add(conf[i].tempId, conf[i]);
        }
    }

    public SkillConfigData getSkillConfig(int id)
    {
        SkillConfigData data = null;
        if (dict.ContainsKey(id))
        {
            data = dict[id];
        }
        return data;
    }

    public Dictionary<int, SkillConfigData> getSkillConfigAll()
    {
        return this.dict;
    }

}

