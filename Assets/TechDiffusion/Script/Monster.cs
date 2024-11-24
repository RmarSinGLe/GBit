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
        //���ⴰ��

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
        Debug.Log("����ʱ������ִ���¼���");
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; // ���¾�����ʱ��
            float t = elapsedTime / duration; // �����ֵ����
            Vector3 currentPosition = Vector3.Lerp(myPosition, aimPosition, t);
            transform.position = currentPosition; // ���������λ��
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
