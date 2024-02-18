using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    public AudioClip backgroundAudio; // 배경 사운드 파일
    private AudioSource audioSource;

    public float volume = 0.2f; // 조절할 볼륨 값 (0.0f부터 1.0f까지)

    void Start()
    {
        // AudioSource 가져오기 또는 추가하기
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // AudioClip 설정
        audioSource.clip = backgroundAudio;

        // 루프 재생 설정
        audioSource.loop = true;

        // 볼륨 설정
        audioSource.volume = volume;

        // 배경 사운드 재생
        audioSource.Play();
    }
}

