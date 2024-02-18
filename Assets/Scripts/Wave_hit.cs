using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_hit : MonoBehaviour
{
    Rigidbody2D rigid;

    public float maxSpeed;
    public float moveSpeed = 5f; // 파도의 이동 속도

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigid.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
    
        if(transform.position.y < -2f)
        {
            DestroyWave();
        }
    }

    void DestroyWave()
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
