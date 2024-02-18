using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFireChiken : MonoBehaviour
{
    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Destroy()
    {
        Debug.Log("DestroyChicken function called.");

        // 파괴 전에 사용할 로직 추가

        // 파괴 전에 Rigidbody2D를 사용하지 않도록 중지
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0f;

        // 파괴
        DestroyFire();
    }

    void FixedUpdate()
    {
        if (transform.position.y < -2f)
        {
            DestroyFire();
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Platform")) // 예시: 바닥과 충돌했을 때
        {
            // 충돌이 발생한 후 3초 후에 DestroyObject 함수를 호출하여 객체를 비활성화
            Invoke("DestroyFire", 3f);
        }

        if (collision.gameObject.CompareTag("Player")) // 예시: 플레이어와 충돌했을 때
        {
            Debug.Log("이거 먹어는 진 것 같음 ㄹㅇㅋㅋ");
            DestroyFire();
        }
    }

    void DestroyFire()
    {
        // 파괴 전에 사용할 로직 추가

        // 파괴 전에 Rigidbody2D를 사용하지 않도록 중지
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0f;

        // 파괴
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        rigid = null;
    }
}
