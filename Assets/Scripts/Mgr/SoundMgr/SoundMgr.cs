using System.Collections;
using System.Collections.Generic;
using ChuMeng;
using UnityEngine;




public class SoundInfo
{
    public int tempId;
    public string name;
    public string path;
    public bool loop;
}

public class SoundMgr : Singleton<SoundMgr>
{
    //k 音效名字 ， v 音效
    private Dictionary<string, SoundInfo> dictSound = null;


    public override void init()
    {
        dictSound = new Dictionary<string, SoundInfo>();
        initConfig();
    }

    /// <summary>
    /// 切换音效 
    /// </summary>
    /// <param name="audioEffectName"></param>
    public AudioClip PlayAudioEffect(string audioEffectName)
    {
        AudioClip s = null;
        if (dictSound.ContainsKey(audioEffectName))
        {
            s = ResMgr.Instance.LoadAudioClip(dictSound[audioEffectName].path);
        }
        return s;
    }
    /// <summary>
    /// 播放/切换音效
    /// </summary>
    /// <param name="tempKey"></param>
    /// <param name="pos"></param>
    public void playAudioPoint(string tempKey, Vector3 pos)
    {
        if (!dictSound.ContainsKey(tempKey))
            return;

        SoundInfo info = dictSound[tempKey];
        AudioClip clip = Resources.Load<AudioClip>(info.path);
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos);
        }
    }
    /// <summary>
    /// 播放/切换背景音乐
    /// </summary>
    /// <param name="tempKey"></param>
    public void playAudioBg(string tempKey)
    {
        if (!dictSound.ContainsKey(tempKey))
            return;

        SoundInfo info = dictSound[tempKey];
        AudioClip clip = Resources.Load<AudioClip>(info.path);
        if (clip != null)
        {
            AudioSource AS = DDOLObj.Instance.gameObject.GetComponent<AudioSource>();
            if (AS == null)
                AS = DDOLObj.Instance.gameObject.AddComponent<AudioSource>();
            AS.clip = clip;
            AS.volume = 0.5f;
            AS.Play();
            AS.loop = info.loop;
        }
    }

    private void initConfig()
    {
        List<SoundConfigData> lst = new List<SoundConfigData>(GameData.SoundConfig);
        for (int i = 0; i < lst.Count; i++)
        {
            SoundInfo dt = new SoundInfo();
            dt.name = lst[i].name;
            dt.path = lst[i].Path;
            dt.loop = lst[i].loop;
            dt.tempId = lst[i].tempId;

            dictSound.Add(dt.name, dt);
        }
    }

}
