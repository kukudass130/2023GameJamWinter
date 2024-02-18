using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoin : MonoBehaviour
{
    public GameObject manager;
    public int count;

    private GameObject[] coins;

    private void Start()
    {
        coins = new GameObject[count];
        for(int i=0; i < count; i++)
        {
            coins[i] = manager.transform.GetChild(i).gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Money")
        {
            Debug.Log("Gain the coin!");
            Destroy(other.gameObject);
            coins[count-1].SetActive(false);
            count--;
            Debug.Log("Count down");
        }
    }
}
