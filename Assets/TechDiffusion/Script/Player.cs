using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // 移动速度
    public float jumpForce = 5f; // 跳跃力度
    public int jumpCount =2; // 跳跃计数器
    private Rigidbody2D rb; // 用于处理物理的组件
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
        rb = GetComponent<Rigidbody2D>(); // 获取 Rigidbody2D 组件
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
        rb.velocity = movement; // 应用水平移动
    }
    /*如果一开始角色在空中的话并且按跳跃键的话，地面检测会失效，失效原因未知*/
    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpCount++;
            if (jumpCount < 2) // 检查是否可以跳跃
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce); // 应用跳跃力度
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
            //Debug.Log("每秒触发一次的代码执行了！");
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
        transform.position = checkpoint.position; // 将角色位置设置为存档点的位置
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
