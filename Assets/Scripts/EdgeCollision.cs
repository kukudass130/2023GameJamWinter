using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeCollision : NewBehaviourScript
{
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Platform"))
        {
            isJumping = false;
            Debug.Log("¾Æ·§¸éÀÌ ºÎµúÈû");
        }
    }
}
