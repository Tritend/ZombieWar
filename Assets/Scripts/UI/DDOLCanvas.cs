using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DDOLCanvas : MonoBehaviour
{

    public static DDOLCanvas Instance;
    public bool isOnLoad = false;
    private Canvas canvas = null;
    private GameObject bg;
    private Image loadImg;
    private float fillAmount = 0;
    private float endFill = 0;
    private void Awake()
    {
        MonoBehaviour.DontDestroyOnLoad(this.gameObject);
        canvas = this.GetComponent<Canvas>();
        bg = this.transform.Find("bg").gameObject;
        loadImg = this.transform.Find("bg/loadBg/loadImg").GetComponent<Image>();
        hideLoadCnavas();
        Instance = this;
    }

    private void Update()
    {
        if (canvas.worldCamera == null && isOnLoad)
        {
            if (Camera.main != null)
            {
                canvas.worldCamera = Camera.main;
                canvas.planeDistance = 1;
            }
        }
        if (isOnLoad)
        {
            fillAmount += Time.deltaTime;
            fillAmount = Mathf.Lerp(fillAmount, endFill, 0.25f);
            loadImg.fillAmount = fillAmount;
        }
    }

    public void setFill(float fill)
    {
        endFill = fill;
    }

    public void showLoadCnavas()
    {
        bg.gameObject.SetActive(true);
        isOnLoad = true;
    }
    public void hideLoadCnavas()
    {
        bg.gameObject.SetActive(false);
        isOnLoad = false;
    }


}
