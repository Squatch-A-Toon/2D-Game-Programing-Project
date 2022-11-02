using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Player>() == null)
            return;

        if (collision.contacts[0].normal.y > 0)
            TakeHit();
    }

    private void TakeHit()
    {
        var particalSystem = GetComponent<ParticleSystem>();
        particalSystem.Play();

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent <Collider2D>().enabled = false;
    }
}
