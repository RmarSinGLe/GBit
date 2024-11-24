using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Vector3 aimPosition;
    public Vector3 myPosition;
    public float restTime = 10;
    public float speed = 0.5f;
    public bool isRest = true;
    public Player player;
    public float sanDDL = 50;
    public float DispairSan = 80;
    public float countdownDuration = 5f;

    public float duration = 2f; 
    private float elapsedTime = 0f; 

    void Start()
    {
        aimPosition=player.transform.position;
        myPosition=transform.position;
        gameObject.SetActive(false);
    }

    
    void Update()
    {
        if(player.san<=sanDDL)
        {
            gameObject.SetActive(true);
        }
        else if(player.san>DispairSan)
        {
            gameObject.SetActive(false);
        }
        //特殊窗口

        if(!isRest)
        {
            StartCoroutine(ExecuteAfterTime(restTime));
        }
    }
    private IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Attack();
    }

    private void Attack()
    {
        Debug.Log("倒计时结束，执行事件！");
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; // 更新经过的时间
            float t = elapsedTime / duration; // 计算插值因子
            Vector3 currentPosition = Vector3.Lerp(myPosition, aimPosition, t);
            transform.position = currentPosition; // 更新物体的位置
        }
    }
    public void ChasePlay()
    {
        if(isRest)
        {
            Vector2 direction = (aimPosition - transform.position).normalized;
            Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.deltaTime;
            transform.position = newPosition;
        }
        
    }
}
