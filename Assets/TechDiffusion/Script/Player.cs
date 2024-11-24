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
    public float rareOfDesan = 1;//per second
    public bool isDesan = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // ��ȡ Rigidbody2D ���
        col = GetComponent<Collider2D>();
        StartCoroutine(Desan());
    }

    void Update()
    {
        Move();
        Jump();

    }
    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement; // Ӧ��ˮƽ�ƶ�
    }

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
        RaycastHit2D hit= Physics2D.Raycast(origin,Vector2.down,0.1f);
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
                san = san - rareOfDesan;
            }
            Debug.Log("ÿ�봥��һ�εĴ���ִ���ˣ�");
            yield return new WaitForSeconds(1f);
        }
    }
}
