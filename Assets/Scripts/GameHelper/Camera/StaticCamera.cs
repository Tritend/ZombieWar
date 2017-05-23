using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class StaticCamera : MonoBehaviour
{
    private Camera myCamera;
    private GameObject blood;
    private GameObject dieBg;
    private GameObject slot;
    private Image hpImg;
    private Text countDownText;
    private Tweener tweener = null;
    private Dictionary<int, CrystalHPUIItem> showDict = new Dictionary<int, CrystalHPUIItem>();
    private List<CrystalHPUIItem> unUseLst = new List<CrystalHPUIItem>();

    private void Awake()
    {
        MessageCenter.Instance.addListener(MsgCmd.On_HP_Change_Value, onHPChange);
    }

    private void Start()
    {
        this.GetComponent<Canvas>().planeDistance = 0.3f;
        slot = this.transform.Find("Main/crystal/content/crystalItem").gameObject;
        slot.SetActive(false);
        blood = this.transform.Find("blood").gameObject;
        dieBg = this.transform.Find("dieBg").gameObject;
        hpImg = this.transform.Find("Main/HP").GetComponent<Image>();
        countDownText = this.transform.Find("countText").GetComponent<Text>();
        blood.SetActive(false);
        dieBg.SetActive(false);
        blood.GetComponent<Image>().color = new Color(1, 0, 0, 0);
        MessageCenter.Instance.addListener(MsgCmd.On_Crystal_HP_Change, onCrystalHPChange);
    }

    private void Update()
    {
        if (myCamera == null)
        {
            myCamera = Camera.main;
            if (myCamera != null)
            {
                this.GetComponent<Canvas>().worldCamera = myCamera;
            }
        }
    }

    public void onDamage()
    {
        blood.SetActive(true);
        if (tweener == null)
            tweener = blood.GetComponent<Image>().DOColor(new Color(1, 0, 0, 1), 0.8f).OnComplete(() =>
              {
                  blood.GetComponent<Image>().DOColor(new Color(1, 0, 0, 0), 1f).OnComplete(() =>
                  {
                      blood.SetActive(false);
                      tweener = null;
                  });
              });
    }
    private void onHPChange(Message msg)
    {
        float hp = (float)msg["HP"];
        float orghp = (float)msg["OrgHP"];
        float fill = (hp / orghp) <= 0 ? 0 : hp / orghp;
        hpImg.DOFillAmount(fill, 0.2f);
    }
    private void onCrystalHPChange(Message msg)
    {
        int id = (int)msg["id"];
        float hp = (float)msg["hp"];
        float orgHp = (float)msg["orgHP"];
        if (showDict.ContainsKey(id))
        {
            CrystalHPUIItem crystal = showDict[id];
            float fill = hp / orgHp;
            if (fill <= 0)
            {
                GameObject.Destroy(crystal.gameObject);
                showDict.Remove(id);
            }
            else
            {
                crystal.setFill(hp / orgHp);
            }
        }
        else
        {
            if (slot != null)
            {
                GameObject go = MonoBehaviour.Instantiate(slot, slot.transform.parent) as GameObject;
                go.SetActive(true);
                CrystalHPUIItem crystal = go.AddComponent<CrystalHPUIItem>();
                crystal.setFill(hp / orgHp);
                showDict.Add(id, crystal);
            }
        }
    }
    //倒计时
    public void setCountDown(int count, string desc)
    {
        StartCoroutine(countDown(count, desc));
    }
    private IEnumerator countDown(int count, string desc)
    {
        countDownText.gameObject.SetActive(true);
        countDownText.text = desc + "\n" + "倒计时结束" + count;
        while (count > 0)
        {
            yield return new WaitForSeconds(1f);
            count--;
            countDownText.text = desc + "\n" + "倒计时结束" + count;
        }
        countDownText.gameObject.SetActive(false);
        yield break;
    }
}

