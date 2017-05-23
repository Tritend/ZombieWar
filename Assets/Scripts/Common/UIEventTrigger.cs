using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIEventTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Action onClickHandler = null;
    private Action onEnterHandler = null;
    private Action onExitHandler = null;
    private float clickCD = 0.5f;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onClickHandler != null)
        {
            onClickHandler();
            this.enabled = false;
            TimeMgr.Instance.setTimerHandler(new TimerHandler(clickCD, () => { if (this != null) this.enabled = true; }));
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnterHandler != null)
        {
            onEnterHandler();
        }
        this.transform.localScale = new Vector3(1.2f, 1.2f, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (onExitHandler != null)
        {
            onExitHandler();
        }
        this.transform.localScale = new Vector3(1f, 1f, 1);
    }

    public void setClickHandler(Action handler)
    {
        onClickHandler = handler;
    }
    public void setEnterHandler(Action handler)
    {
        onEnterHandler = handler;
    }
    public void setExitHandler(Action handler)
    {
        onExitHandler = handler;
    }

}

