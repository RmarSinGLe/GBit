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

    public float dashSpeed = 20f; // ����ٶ�
    public float dashDuration = 1f; // ��̳���ʱ��
    private float dashCooldown = 10f; // �����ȴʱ��
    private float dashTimer = 0f; // ���ڼ�����ȴʱ��
    private bool isDashing = false; // �����Ƿ��ڳ��״̬

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
            Debug.Log("����ʱ������ִ���¼���");
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
        
        if (player != null)
        {
            Vector3 targetDir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
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
//        EventManager.Instance.onMonsterActivate.RemoveListener(MonsterStateChange);
    }
}