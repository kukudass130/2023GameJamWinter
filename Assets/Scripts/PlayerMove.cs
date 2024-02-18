using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameManager gameManager;
    public float maxSpeed;
    private float basedSpeed = 5; // 현재 최대 속도
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer SpriteRenderer;
    Animator anim;
    public bool isJumping;
    CapsuleCollider2D capsule_pl;
    public bool fallOut = false;
    bool isPowerUp; // 아이템을 먹었을 때의 상태 여부를 나타내는 변수

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
        // 아이템을 먹은 경우 3초 동안 최대 속도를 2배로 증가
        if (isPowerUp)
        {
            StartCoroutine(PowerUpTimer(3f));
        }

        // 점프
        if (Input.GetButtonDown("Jump") && !isJumping) { 
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJumping = true;
            PlaySound("JUMP");
        }
        // 속도 고정
        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        // 스프라이트 방향
        if (Input.GetButton("Horizontal"))
            SpriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        
        // 애니메이션
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
            Debug.Log("플레이어가 맞음");
        }

        if (collision.gameObject.tag == "FireChiken")
        {
            Debug.Log("불닭을 먹음!");
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

            // 아이템 비활성화
            collision.gameObject.SetActive(false);

            PlaySound("ITEM");
        }
    }

    void OnDamaged(Vector2 targetPos)
    {
        // 체력 감소
        gameManager.HpDown();

        // 레이어 변경
        gameObject.layer = 11;

        // 투명화
        //SpriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // 피격 시 반응
        int direction = targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(direction * 10, 0.5f) * 100, ForceMode2D.Impulse);

        // 애니메이션
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
        // 움직이기
        float h = Input.GetAxisRaw("Horizontal")*20;

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // 최대 속도 지정
        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if(rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }

    // 아이템을 먹었을 때의 효과를 부여하는 코루틴
    IEnumerator PowerUpTimer(float duration)
    {
        // 최대 속도를 2배로 증가
        maxSpeed *= 2f;

        PlaySound("SPRINT");

        // 3초 동안 대기
        yield return new WaitForSeconds(duration);

        // 대기 후 최대 속도를 원래대로 복구
        maxSpeed = basedSpeed;

        // 상태 변수 초기화
        isPowerUp = false;
    }

    // 아이템을 먹었을 때 호출되는 함수
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
