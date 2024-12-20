using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // 移动速度
    public float jumpForce = 5f; // 跳跃力度
    public int jumpCount =2; // 跳跃计数器
    private Rigidbody2D rb; 
    private Collider2D col;
    private SpriteRenderer rbSprite;
    public float san = 100;
    public float fallDownDamage = 50;
    public float flowerSanReply = 10;
    public float monsterDamage = 10;
    public float checkpointSanReply = 10;
    public float rareOfDesan = 1;//per second
    public bool isDesan = true;
    public Transform checkpoint;
    public List<Transform> checkPointList;
    public int pointIndex = 0;
    public float saveSan = 100;

    public MonsterManager monsterManager;

    public GameObject gameOverPanel; // 存放游戏结束面板
    public Button restartButton; // 复活按钮
    public Button quitButton; // 退出按钮

    public float invincibilityDuration = 1f; // 无敌时间
    private bool isInvincible = false; // 无敌状态标志
    private Vector2 knockbackForce; // 碰撞弹开的方向力

    private Animator animator; 

    void Start()
    {
        isDesan = true;
        rb = GetComponent<Rigidbody2D>(); // 获取 Rigidbody2D 组件
        col = GetComponent<Collider2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        StartCoroutine(Desan());

        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
        gameOverPanel.SetActive(false); // 初始化时隐藏面板

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
        // 定义射线的起点
        Vector3 origin = col.bounds.center + new Vector3(0, -col.bounds.extents.y, 0);
        // 定义射线的长度
        float rayLength = 0.3f;
        // 定义射线偏移量
        float rayOffset = col.bounds.extents.x / 3; // 将底部分成3个部分

        // 创建射线进行地面检测
        for (int i = -1; i <= 1; i++)
        {
            Vector3 rayOrigin = origin + new Vector3(i * rayOffset, 0, 0);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, 1 << 7);
            Debug.DrawLine(rayOrigin, rayOrigin + Vector3.down * rayLength, Color.red);

            if (hit.collider != null && hit.collider.CompareTag("Ground"))
            {
                return true; // 如果任意一条射线检测到Ground，则返回true
            }
        }
        return false; // 如果没有射线检测到Ground，则返回false
    }

    private IEnumerator Desan()
    {
        while(true)
        {
            if(isDesan && san>0) 
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

        if (collision.gameObject.tag == "Monster" && !isInvincible)
        {
            Debug.Log("Monster hit me!!!");
            san -= monsterDamage;
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            //
            knockbackForce = direction * 5f;
            StartCoroutine(InvincibilityCoroutine());   
        }

        if(collision.gameObject.tag =="Checkpoint")
        {
            CheckPoint cp= collision.gameObject.GetComponent<CheckPoint>();
            if (!cp.isVisited)
            {
                checkpoint =cp.transform;
                san += checkpointSanReply;
                checkPointList.Add(cp.transform);
                saveSan = san;
            }
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
        if (checkpoint != null)
        {
            san = saveSan;
            transform.position = checkpoint.position;
        }
        else
        {
            checkpoint = checkPointList[pointIndex];
            transform.position = checkpoint.position;
        }
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
            //reStart();
            //Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            //SceneManager.LoadScene("DeathScene");
        }
    }

    public void reStart()
    {
        
    }


    private void RestartGame()
    {
        Debug.Log("复活按钮被点击！");
        Time.timeScale = 1; // 恢复游戏
        san = saveSan; // 复活时恢复生命值
        ResetToCheckpoint(); // 恢复到存档点
        gameOverPanel.SetActive(false); 
    }

    private void QuitGame()
    {
        // 返回主菜单或者退出游戏，这里可以根据实际需求修改
        Time.timeScale = 1; 
        SceneManager.LoadScene("StartScene"); 
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        Color originalColor = rbSprite.color; 
        float flashDuration = 0.1f; 
        int flashCount = 10; 

        // 进行闪烁
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
            // 如果处于无敌状态，应用弹开效果
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