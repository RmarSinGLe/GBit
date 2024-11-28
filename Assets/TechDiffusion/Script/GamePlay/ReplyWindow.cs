using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplyWindow : Windows
{
    TriggerPoint tp;

    public bool inReplyZone=false;
    public Player player;
    public float valueOfRelply = 2.0f; //per second
    private bool isCoroutineRunning = false;

    void Start()
    {
        WindowType();
        Unlock();
    }


    void Update()
    {

    }

    public override void WindowType()
    {
        isReplyWindow = true;
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

    

    private void OnTriggerStay2D(Collider2D collision)
    {
        inReplyZone = true;
        if(collision.gameObject.tag=="Player"&& !isCoroutineRunning)
        {
            StartCoroutine(ReplySan());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            inReplyZone = false;
        }
    }

    private IEnumerator ReplySan()
    {
        isCoroutineRunning = true;
        while (true)
        {
            if (inReplyZone)
            {
                player.san += valueOfRelply;
            }
            //Debug.Log("每秒触发一次的回san执行了！");
            yield return new WaitForSeconds(1f);
        }
        isCoroutineRunning = false;
    }
}
