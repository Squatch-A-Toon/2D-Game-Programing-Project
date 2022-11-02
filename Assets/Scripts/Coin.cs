using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int CoinsCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;
        //Sets coin image to false
        gameObject.SetActive(false);
        //add coin to coins collected
        CoinsCollected++;
        Debug.Log(CoinsCollected);
        //Adds {10} to score
        ScoreSystem.Add(10);
    }
}
