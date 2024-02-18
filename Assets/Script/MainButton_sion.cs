using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButton_sion : MonoBehaviour
{
    public GameObject startButton;
    public GameObject endButton;
    public GameObject settings;
    public GameObject settingWindow;

    public void StartButton()
    {
        PlayerPrefs.DeleteAll(); //플레이어 프리팹 초기화
        Debug.Log("Push Start");
        SceneManager.LoadScene("StageSelect");
    }

    public void ExitGame()
    {
        Debug.Log("Push Exit");
        Application.Quit();
    }

    public void Setting()
    {
        Debug.Log("PushSettings");
        startButton.SetActive(false);
        endButton.SetActive(false);
        settings.SetActive(false);
        settingWindow.SetActive(true);
    }

    public void fnishset()
    {
        Debug.Log("PushXButton");
        settingWindow.SetActive(false);
        startButton.SetActive(true);
        endButton.SetActive(true);
        settings.SetActive(true);
        
    }
}
