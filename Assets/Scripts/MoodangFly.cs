using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoodangFly : MonoBehaviour
{
    public float flySpeed = 0.02f;
    public GameObject itemPrefab; // ������ ������
    public float itemSpawnInterval = 2.0f; // ������ ���� ����
    public int itemSpawnCount = 0;
    public int maxSpawnCount = 3;

    void Start()
    {
        // �ֱ������� SpawnMoodang �Լ��� ȣ���Ͽ� ��ü�� ����
        if(itemSpawnCount < 3)
            Invoke("SpawnItem", 0f);
    }

    void Update()
    {
        Vector2 moveDirection = new Vector2(flySpeed * (-1), 0f);  
        transform.Translate(moveDirection * Time.deltaTime);
    }

    void SpawnItem()
    {
        if (itemPrefab == null)
        {
            Debug.LogError("itemPrefab�� null�Դϴ�.");
            return;
        }

        // ������ �������� ���� Moodang ��ġ�� ����
        Instantiate(itemPrefab, transform.position, transform.rotation);
        itemSpawnCount++;

        // Ư�� Ƚ����ŭ ȣ���ϸ� �� �̻� ȣ������ �ʵ��� ����
        if (itemSpawnCount < maxSpawnCount)
        {
            Invoke("SpawnItem", itemSpawnInterval);
        }

    }
}
