using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tmpGameManager : MonoBehaviour
{
    private int nowStage;
    public GetCoin getCoin;

    private void Start()
    {
        nowStage = PlayerPrefs.GetInt("levelReached");
        Debug.Log(nowStage);
    }
    // Update is called once per frame
    void Update()
    {
        if (getCoin.count == 0)
        {
            Debug.Log("update tmpManager");
            nowStage += 1;
            PlayerPrefs.SetInt("levelReached", nowStage);
            //Nextâ ����
            Debug.Log("���� Ŭ����");
            SceneManager.LoadScene(nowStage);
            
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
