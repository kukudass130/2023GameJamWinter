using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [SerializeField]
    public GameManager GameManager;
    public GameObject Coin;
    public GameObject HP;

    private GameObject[] coins;
    private GameObject[] HPs;
    private int HPCount;
    private int CoinCount;

    // Start is called before the first frame update
    void Start()
    {
        CoinCount = Coin.transform.childCount;
        HPCount = HP.transform.childCount;
        
        coins = new GameObject[CoinCount];
        HPs = new GameObject[HPCount];

        for (int i = 0; i < CoinCount; i++)
        {
            coins[i] = Coin.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < HPCount; i++)
        {
            HPs[i] = HP.transform.GetChild(i).gameObject;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.hp < HPCount)
        {
            HPs[HPCount -1].SetActive(false);
            HPCount--;
        }

    }
}
