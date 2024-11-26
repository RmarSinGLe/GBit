using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMonsterWindow : Windows
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
       isEndWindow = true;
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
        Debug.Log("it's monster end window");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            monsterManager.monsterWindow = false;
        }
    }



}
