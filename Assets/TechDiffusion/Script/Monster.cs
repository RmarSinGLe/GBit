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
    private float elapsedTime = 0f;

    void Start()
    {
        myPosition=transform.position;
        gameObject.SetActive(false);
        EventManager.Instance.onMonsterActivate.AddListener(MonsterStateChange);
    }

    
    void Update()
    {
        ChasePlay();
        if (!isRest)
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
        /*if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; // 更新经过的时间
            float t = elapsedTime / duration; // 计算插值因子
            Vector3 currentPosition = Vector3.Lerp(myPosition, aimPosition, t);
            transform.position = currentPosition; // 更新物体的位置
        }*/
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