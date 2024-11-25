using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Vector3 aimPosition;
    public Vector3 myPosition;

    public float restTime = 10;
    public float speed = 10f;
    public bool isRest = true;
    public Player player;
    public float countdownDuration = 5f;

    public float duration = 2f;

    public float dashSpeed = 20f; // 冲刺速度
    public float dashDuration = 1f; // 冲刺持续时间
    private float dashCooldown = 10f; // 冲刺冷却时间
    private float dashTimer = 0f; // 用于计算冷却时间
    private bool isDashing = false; // 怪物是否处于冲刺状态

    void Start()
    {
        myPosition=transform.position;
        gameObject.SetActive(true);
        EventManager.Instance.onMonsterActivate.AddListener(MonsterStateChange);
        dashTimer = 0f;
    }

    
    void Update()
    {
        if (isDashing)
        {
            Debug.Log("倒计时结束，执行事件！");
            DashTowardsPlayer(); 
        }
        else
        {
            ChasePlay(); 
            if (dashTimer >= dashCooldown)
            {
                StartCoroutine(ExecuteDash());
            }
        }
        dashTimer += Time.deltaTime;

    }

    private IEnumerator ExecuteDash()
    {
        isDashing = true; 
        dashTimer = 0f; 

        float elapsed = 0f;

        while (elapsed < dashDuration) 
        {
            elapsed += Time.deltaTime; 
            yield return null; 
        }

        isDashing = false; 
    }

    private void DashTowardsPlayer()
    {
        if (player != null)
        {
            aimPosition = player.transform.position; 
            Vector2 direction = (aimPosition - transform.position).normalized; 

            transform.position = Vector2.MoveTowards(transform.position, aimPosition, dashSpeed * Time.deltaTime);
        }
    }

    private IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        
    }

    public void ChasePlay()
    {
        if(isRest)
        {
            aimPosition = player.transform.position;
            Vector2 direction = (aimPosition - transform.position).normalized;
            Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.deltaTime;
            transform.position = newPosition;
        }
        
    }
    
    private void MonsterStateChange(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    private void OnDestroy()
    {
        EventManager.Instance.onMonsterActivate.RemoveListener(MonsterStateChange);
    }
}