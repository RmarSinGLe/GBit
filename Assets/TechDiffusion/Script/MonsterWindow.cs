using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWindow : Windows
{
    TriggerPoint tp;
    public MonsterManager monsterManager;

    void Start()
    {

    }

    void Update()
    {

    }

    public override void WindowType()
    {
        isMonsterTrriger = true;
    }

    public override void Unlock()
    {
        isUnlocked = true;
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

   
    public override void WindowFunc()
    {
        Debug.Log("it's monster window");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            monsterManager.monsterWindow = true;
        }
    }



}
