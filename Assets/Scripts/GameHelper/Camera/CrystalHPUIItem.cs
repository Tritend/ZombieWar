using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalHPUIItem : MonoBehaviour
{
    private Image hp;
    private Text hpText;
    private float fill = -1;

    private void Start()
    {
        hp = this.transform.Find("blood").GetComponent<Image>();
        hpText = this.transform.Find("blood/bloodText").GetComponent<Text>();
        if (fill != -1)
        {
            resetFill();
        }
    }


    public void setFill(float fill)
    {
        this.fill = fill;
        resetFill();
    }
    private void resetFill()
    {
        if (hp != null)
        {
            hp.fillAmount = this.fill;
            hpText.text = (this.fill * 100).ToString("0.00") + "%";
        }
    }

}

