using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] Lock _keyLock;
    [SerializeField] int _unlocksRemaining = 2;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if(player != null)
        {
            transform.SetParent(player.transform);
            transform.localPosition = Vector3.up;
        }

        var keyLock = collision.GetComponent<Lock>();
        if (keyLock != null && keyLock == _keyLock && _unlocksRemaining > 0)
        {
            keyLock.Unlock();
            _unlocksRemaining--;
           
            if (_unlocksRemaining >= 0)
            { 
                Destroy(gameObject); 
            }
        }
    }
}
