using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWindow : Windows
{
    public Windows[] delWindows;
    TriggerPoint endPoint;
    
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
            DelWin();
            foreach (var windows in nextWindows)
            {
                windows.gameObject.SetActive(true);
            }
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
