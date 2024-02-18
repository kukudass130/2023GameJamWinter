using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    public AudioClip backgroundAudio; // ��� ���� ����
    private AudioSource audioSource;

    public float volume = 0.2f; // ������ ���� �� (0.0f���� 1.0f����)

    void Start()
    {
        // AudioSource �������� �Ǵ� �߰��ϱ�
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // AudioClip ����
        audioSource.clip = backgroundAudio;

        // ���� ��� ����
        audioSource.loop = true;

        // ���� ����
        audioSource.volume = volume;

        // ��� ���� ���
        audioSource.Play();
    }
}

