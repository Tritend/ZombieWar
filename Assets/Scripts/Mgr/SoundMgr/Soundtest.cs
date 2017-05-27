using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtest : MonoBehaviour
{


    //背景音乐 可切换
    private AudioSource bgAudioSource;
    //音效
    private AudioSource audioSourceEffect;

    void Awake()
    {
        bgAudioSource= gameObject.AddComponent<AudioSource>();
        audioSourceEffect= gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        bgAudioSource.clip = SoundMgr.Instance.PlayAudioEffect("");
        bgAudioSource.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("dddd");
            if (audioSourceEffect.clip == null)
            {
                audioSourceEffect.clip = SoundMgr.Instance.PlayAudioEffect("");
                Debug.Log("cccc");
            }
            audioSourceEffect.Play();
        }

    }

}
