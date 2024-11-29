using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // �ƶ��ٶ�
    public float jumpForce = 5f; // ��Ծ����
    public int jumpCount =2; // ��Ծ������
    private Rigidbody2D rb; // ���ڴ�����������
    private Collider2D col;
    private SpriteRenderer rbSprite;
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

    public GameObject gameOverPanel; // �����Ϸ�������
    public Button restartButton; // ���ť
    public Button quitButton; // �˳���ť

    public float invincibilityDuration = 1f; // �޵�ʱ��
    private bool isInvincible = false; // �޵�״̬��־
    private Vector2 knockbackForce; // ��ײ�����ķ�����

    private Animator animator; 

    void Start()
    {
        isDesan = true;
        rb = GetComponent<Rigidbody2D>(); // ��ȡ Rigidbody2D ���
        col = GetComponent<Collider2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        StartCoroutine(Desan());

        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
        gameOverPanel.SetActive(false); // ��ʼ��ʱ�������

        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        resetPosition();
        knockAway();
    }
    void Update()
    {
        Move();
        Jump();
        monsterManager.UpdateMonstersState();
        gameOver();

        UpdateAnimation();
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
        //Debug.Log(hit.collider.tag);
        if (hit.collider != null && hit.collider.tag=="Ground") return true;
        else  return false;
    }

    private IEnumerator Desan()
    {
        while(true)
        {
            if(isDesan && san>0) 
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

        if (collision.gameObject.tag == "Monster" && !isInvincible)
        {
            Debug.Log("Monster hit me!!!");
            san -= monsterDamage;
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            knockbackForce = direction * 10f;
            StartCoroutine(InvincibilityCoroutine());   
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
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
    }

    public void reStart()
    {
        san = saveSan;
        transform.position = checkpoint.position;
    }


    private void RestartGame()
    {
        Debug.Log("���ť�������");
        Time.timeScale = 1; // �ָ���Ϸ
        san = saveSan; // ����ʱ�ָ�����ֵ
        ResetToCheckpoint(); // �ָ����浵��
        gameOverPanel.SetActive(false); 
    }

    private void QuitGame()
    {
        // �������˵������˳���Ϸ��������Ը���ʵ�������޸�
        Time.timeScale = 1; 
        SceneManager.LoadScene("StartScene"); 
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        Color originalColor = rbSprite.color; 
        float flashDuration = 0.1f; 
        int flashCount = 10; 

        // ������˸
        for (int i = 0; i < flashCount; i++)
        {
            rbSprite.color = Color.red; 
            yield return new WaitForSeconds(flashDuration); 
            rbSprite.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }

        rbSprite.color = originalColor;
        yield return new WaitForSeconds(invincibilityDuration - (flashCount * flashDuration));
        isInvincible = false;
    }

    public void knockAway()
    {
        if (isInvincible)
        {
            // ��������޵�״̬��Ӧ�õ���Ч��
            rb.velocity = new Vector2(knockbackForce.x, rb.velocity.y);
        }
    }

    private void UpdateAnimation()
    {
        float moveInput = Input.GetAxis("Horizontal");
        animator.SetBool("IsRuning", moveInput != 0); 

        animator.SetBool("IsJumping", !IsGrounded());

        animator.SetBool("IsIdle", moveInput == 0 && IsGrounded());

        if (moveInput > 0)
        {
            rbSprite.flipX = false;
        }
        else if (moveInput < 0)
        {
            rbSprite.flipX = true;
        }
    }
}