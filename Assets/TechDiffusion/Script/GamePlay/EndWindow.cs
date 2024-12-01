using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWindow : Windows
{
    public Windows[] delWindows;
    TriggerPoint endPoint;
    public Player player;
    private bool hasExecuted = false;

    void Start()
    {
        endPoint = triggerPoint.GetComponent<TriggerPoint>();
        WindowType();
        Unlock();
    }

    void Update()
    {
        TriggerPoint();
    }

    public override void TriggerPoint()
    {
        if (isEndWindow && endPoint.isTrigger && isWindowTrigger)
        {
            if (hasExecuted)
            {
                return; // 已经执行过，直接返回
            }
            DelWin();
            foreach (var windows in nextWindows)
            {
                windows.gameObject.SetActive(true);
            }
            player.pointIndex++;
            hasExecuted = true;
        }
    }

    public override void Unlock()
    {
        isUnlocked = true;
    }

    public override void WindowFunc()
    {
        
    }

    public override void WindowType()
    {
        isWindowTrigger = true;
        isStartWindow = true;
        isEndWindow = true;
    }

    public void DelWin()
    {
        foreach (var win in delWindows)
        {
            if(win!=null) Destroy(win.gameObject);
        }
    }
}
