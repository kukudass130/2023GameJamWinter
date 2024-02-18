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

        // �ı� ���� ����� ���� �߰�

        // �ı� ���� Rigidbody2D�� ������� �ʵ��� ����
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0f;

        // �ı�
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
        if (collision.gameObject.CompareTag("Platform")) // ����: �ٴڰ� �浹���� ��
        {
            // �浹�� �߻��� �� 3�� �Ŀ� DestroyObject �Լ��� ȣ���Ͽ� ��ü�� ��Ȱ��ȭ
            Invoke("DestroyFire", 3f);
        }

        if (collision.gameObject.CompareTag("Player")) // ����: �÷��̾�� �浹���� ��
        {
            Debug.Log("�̰� �Ծ�� �� �� ���� ��������");
            DestroyFire();
        }
    }

    void DestroyFire()
    {
        // �ı� ���� ����� ���� �߰�

        // �ı� ���� Rigidbody2D�� ������� �ʵ��� ����
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0f;

        // �ı�
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        rigid = null;
    }
}
