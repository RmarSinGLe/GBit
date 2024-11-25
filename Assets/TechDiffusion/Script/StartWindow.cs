using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartWindow : Windows
{
    TriggerPoint tp = null;
    void Start()
    {
        tp = triggerPoint.GetComponent<TriggerPoint>();

        WindowType();
        Unlock();
        if(isStartWindow)
        { 
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {

    }
    public override void WindowType()
    {
        isWindowTrigger = true;
        isStartWindow = true;
    }
    public override void Unlock()
    {
        isUnlocked = true;
    }

    public override void WindowFunc()
    {
        Debug.Log("it's nomarl window");
    }
    public override void TriggerPoint()
    {
        


        if(isWindowTrigger&& tp.isTrigger)
        {
            foreach (var windows in nextWindows)
            {
                windows.gameObject.SetActive(true);
            }
        }
    }
    
}
