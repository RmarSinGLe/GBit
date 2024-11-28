using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalWindow : Windows
{
    TriggerPoint tp;

    void Start()
    {
        tp = triggerPoint.GetComponent<TriggerPoint>();
        Unlock();
        WindowType();
    }
    void Update()
    {
        TriggerPoint();
    }
    public override void TriggerPoint()
    {
        if (isWindowTrigger && tp.isTrigger)
        {
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
    }
}
