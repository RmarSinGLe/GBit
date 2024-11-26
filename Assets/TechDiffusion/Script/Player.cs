using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // �ƶ��ٶ�
    public float jumpForce = 5f; // ��Ծ����
    public int jumpCount =2; // ��Ծ������
    private Rigidbody2D rb; // ���ڴ�����������
    private Collider2D col;
    public float san = 100;
    public float fallDownDamage = 50;
    public float flowerSanReply = 10;
    public float monsterDamage = 10;
    public float checkpointSanReply = 1;
    public float rareOfDesan = 1;//per second
    public bool isDesan = true;
    public Transform checkpoint;
    public List<Transform> checkPointList;
    public float saveSan = 100;

    public MonsterManager monsterManager;

    void Start()
    {
        isDesan = true;
        rb = GetComponent<Rigidbody2D>(); // ��ȡ Rigidbody2D ���
        col = GetComponent<Collider2D>();
        StartCoroutine(Desan());
    }
    private void FixedUpdate()
    {
        resetPosition();
    }
    void Update()
    {
        Move();
        Jump();
        monsterManager.UpdateMonstersState();
    }
    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement; // Ӧ��ˮƽ�ƶ�
    }
    /*���һ��ʼ��ɫ�ڿ��еĻ����Ұ���Ծ���Ļ����������ʧЧ��ʧЧԭ��δ֪*/
    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpCount++;
            if (jumpCount < 2) // ����Ƿ������Ծ
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Ӧ����Ծ����
            }
        }
        if(IsGrounded())
        {
            jumpCount = 0;
        }
    }

    private bool IsGrounded()
    {   
        Vector3 origin =col.bounds.center+new Vector3(0,-col.bounds.extents.y,0);
        RaycastHit2D hit= Physics2D.Raycast(origin,Vector2.down,0.1f,1<<7);
        Debug.DrawLine(origin, origin+Vector3.down*0.1f,Color.red);
        Debug.Log(hit.collider.tag);
        if (hit.collider != null && hit.collider.tag=="Ground") return true;
        else  return false;
    }

    private IEnumerator Desan()
    {
        while(true)
        {
            if(isDesan) 
            {
                san =san - rareOfDesan;
            }
            //Debug.Log("ÿ�봥��һ�εĴ���ִ���ˣ�");
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Flower")
        {
            san+= flowerSanReply;
        }

        if (collision.gameObject.tag == "Monster")
        {
            san -= monsterDamage;
        }

        if(collision.gameObject.tag =="Checkpoint")
        {
            san += checkpointSanReply;
            checkPointList.Add(checkpoint);
            checkpoint=collision.transform;
            saveSan = san;
        }
    }
    public void fallDown()
    {
        san -= fallDownDamage;
    }
    public void glitchEffect()
    {

    }

    public void ResetToCheckpoint()
    {
        transform.position = checkpoint.position; // ����ɫλ������Ϊ�浵���λ��
    }

    public void resetPosition()
    {
        if(transform.position.y<-20)
        {
            ResetToCheckpoint();
            fallDown();
        }
    }

    public void gameOver()
    {
        if(san<0)
        {
            reStart();
            //.....
        }
    }

    public void reStart()
    {
        san = saveSan;
        transform.position = checkpoint.position;
    }

}
