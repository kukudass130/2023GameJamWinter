using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoodangFly : MonoBehaviour
{
    public float flySpeed = 0.02f;
    public GameObject itemPrefab; // 아이템 프리팹
    public float itemSpawnInterval = 2.0f; // 아이템 스폰 간격
    public int itemSpawnCount = 0;
    public int maxSpawnCount = 3;

    void Start()
    {
        // 주기적으로 SpawnMoodang 함수를 호출하여 객체를 스폰
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
            Debug.LogError("itemPrefab이 null입니다.");
            return;
        }

        // 아이템 프리팹을 현재 Moodang 위치에 생성
        Instantiate(itemPrefab, transform.position, transform.rotation);
        itemSpawnCount++;

        // 특정 횟수만큼 호출하면 더 이상 호출하지 않도록 설정
        if (itemSpawnCount < maxSpawnCount)
        {
            Invoke("SpawnItem", itemSpawnInterval);
        }

    }
}
