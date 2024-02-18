using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject mobPrefab; // 몹의 프리팹
    public float spawnInterval = 2f; // 몹 생성 간격
    public Transform spawnPoint; // 몹이 생성될 위치

    void Start()
    {
        // 주기적으로 SpawnMob 함수를 호출하여 몹을 생성
        
        InvokeRepeating("SpawnMob", 0f, spawnInterval);
    }

    void SpawnMob()
    {
        if (mobPrefab == null || spawnPoint == null)
        {
            Debug.LogError("mobPrefab 또는 spawnPoint이 null입니다.");
            return;
        }

        Instantiate(mobPrefab, spawnPoint.position, transform.rotation);
    }

}