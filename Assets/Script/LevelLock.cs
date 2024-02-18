using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLock : MonoBehaviour
{
    int openLevel = 1;  //현재 스테이지 번호
    public GameObject stageNum; //스테이지 버튼들

    // Start is called before the first frame update
    void Start()
    {
        Button[] stages = stageNum.GetComponentsInChildren<Button>();

        openLevel = PlayerPrefs.GetInt("levelReached");
        for (int i = openLevel + 1; i < stages.Length; i++)
        {
            stages[i].interactable = false;
            //클리어되지 않은 스테이지(i+1번째 이상) 버튼은 비활성화
        }
    }

}
