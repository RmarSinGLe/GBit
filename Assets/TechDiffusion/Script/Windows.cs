using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Windows : MonoBehaviour
{
    public bool isUnlocked=false;
    public bool isStartWindow=false;
    public bool isEndWindow=false;
    public bool isReplyWindow = false;
    public bool isWindowTrigger=false;
    public bool isMonsterTrriger=false;
    public GameObject triggerPoint=null;
    public Windows[] nextWindows;


    public abstract void Unlock();
    public abstract void WindowType();
    public abstract void WindowFunc();

    public abstract void TriggerPoint();
}
