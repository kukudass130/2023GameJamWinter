using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameManager gameManager;
    public float maxSpeed;
    private float basedSpeed = 5; // ���� �ִ� �ӵ�
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer SpriteRenderer;
    Animator anim;
    public bool isJumping;
    CapsuleCollider2D capsule_pl;
    public bool fallOut = false;
    bool isPowerUp; // �������� �Ծ��� ���� ���� ���θ� ��Ÿ���� ����

    public AudioClip audioBackG;
    public AudioClip audioDamaged;
    public AudioClip audioJump;
    public AudioClip audioDie;
    public AudioClip audioItem;
    public AudioClip audioSprint;

    AudioSource audioSource;
    

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        SpriteRenderer= GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        isJumping = false;
        isPowerUp = false;
        capsule_pl = GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }
    
    

    void Update()
    {
        // �������� ���� ��� 3�� ���� �ִ� �ӵ��� 2��� ����
        if (isPowerUp)
        {
            StartCoroutine(PowerUpTimer(3f));
        }

        // ����
        if (Input.GetButtonDown("Jump") && !isJumping) { 
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJumping = true;
            PlaySound("JUMP");
        }
        // �ӵ� ����
        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        // ��������Ʈ ����
        if (Input.GetButton("Horizontal"))
            SpriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        
        // �ִϸ��̼�
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")) { 
                isJumping = false;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            OnDamaged(collision.transform.position);
            Debug.Log("�÷��̾ ����");
        }

        if (collision.gameObject.tag == "FireChiken")
        {
            Debug.Log("�Ҵ��� ����!");
            isPowerUp = true;
        }

        if (collision.gameObject.CompareTag("DeadLine"))
        {
            fallOut = true;
            OnDie();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            // Point
            gameManager.getLoad += 1;

            // ������ ��Ȱ��ȭ
            collision.gameObject.SetActive(false);

            PlaySound("ITEM");
        }
    }

    void OnDamaged(Vector2 targetPos)
    {
        // ü�� ����
        gameManager.HpDown();

        // ���̾� ����
        gameObject.layer = 11;

        // ����ȭ
        //SpriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // �ǰ� �� ����
        int direction = targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(direction * 10, 0.5f) * 100, ForceMode2D.Impulse);

        // �ִϸ��̼�
        anim.SetTrigger("doDamaged");
        Invoke("OffDamaged", 1);

        PlaySound("DAMAGED");
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        //SpriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void OnDie()
    {

        SpriteRenderer.color = new Color(1, 1, 1, 0.4f);
        SpriteRenderer.flipY = true;
        capsule_pl.enabled = false;
        
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        PlaySound("DIE");

    }
    void FixedUpdate()
    {
        // �����̱�
        float h = Input.GetAxisRaw("Horizontal")*20;

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // �ִ� �ӵ� ����
        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if(rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }

    // �������� �Ծ��� ���� ȿ���� �ο��ϴ� �ڷ�ƾ
    IEnumerator PowerUpTimer(float duration)
    {
        // �ִ� �ӵ��� 2��� ����
        maxSpeed *= 2f;

        PlaySound("SPRINT");

        // 3�� ���� ���
        yield return new WaitForSeconds(duration);

        // ��� �� �ִ� �ӵ��� ������� ����
        maxSpeed = basedSpeed;

        // ���� ���� �ʱ�ȭ
        isPowerUp = false;
    }

    // �������� �Ծ��� �� ȣ��Ǵ� �Լ�
    void PowerUp()
    {
        isPowerUp = true;
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                audioSource.Play();
                break;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                audioSource.Play();
                break;
            case "DIE":
                audioSource.clip = audioDie;
                audioSource.Play();
                break;
            case "ITEM":
                audioSource.clip = audioItem;
                audioSource.Play();
                break;
            case "SPRINT":
                audioSource.clip = audioSprint;
                audioSource.Play();
                break;
            case "BACKG":
                audioSource.clip = audioBackG;
                audioSource.Play();
                break;
        }
    }
}
