using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int getLoad;
    public int stageIndex;
    public int hp;
    public GameObject[] stages;
    public NewBehaviourScript Player;


    public void NextStage()
    {
        if(stageIndex < stages.Length-1)
        {
            // Change Stage
            stages[stageIndex].SetActive(false);
            stageIndex++;
            stages[stageIndex].SetActive(true);
            PlayerReposition();
        }
        else // Game clear
        {
            Time.timeScale = 0;

            Debug.Log("���� Ŭ����");
        }
        ;

        // Calculate Point
        //totolPoint += stagePoint;

    }

    public void HpDown()
    {
        if (hp > 1)
            hp--;
        else
        {
            // ���� ����Ʈ
            Player.OnDie();

            // ���� UI
            Debug.Log("����");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
    }

    void PlayerReposition()
    {
        Player.transform.position = new Vector3(0, 0, -1);
        Player.VelocityZero();
    }

    
}
