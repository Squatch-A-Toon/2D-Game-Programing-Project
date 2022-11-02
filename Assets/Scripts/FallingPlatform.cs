using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Vector3 _initialPosition;
    float _wiggleTimer;

    public bool PlayerInside;

    HashSet<Player> _playersInTrigger = new HashSet<Player>();

    [Tooltip("Resets Wigglt timer when no players are on platform")]
    [SerializeField] bool _resetOnEmpty;
    [SerializeField] bool _falling;
    [SerializeField] float _fallSpeed = 1f;
    [Range(0.1f, 5)] [SerializeField] float _fallAfterSeconds = 3f;
    [Range(-0.05f, 0.05f)] [SerializeField] float _wiggleX = 0;
    [Range(-0.05f, 0.05f)] [SerializeField] float _WiggleY = 0;
   
    private void Start()
    {
        _initialPosition = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;
        _playersInTrigger.Add(player);

        PlayerInside = true;

        StartCoroutine(WiggleAndFall());
    }

    IEnumerator WiggleAndFall()
    {
        Debug.Log("Wait To Wiggle");
        yield return new WaitForSeconds(0.25f);
        Debug.Log("Wiggle");
        //_wiggleTimer = 0;

        while(_wiggleTimer < _fallAfterSeconds)
        {
            float randomX = UnityEngine.Random.Range(-_wiggleX, _wiggleX);
            float randomY = UnityEngine.Random.Range(-_WiggleY, _WiggleY);
            transform.position = _initialPosition + new Vector3(randomX, randomY);
            float randomDelay = UnityEngine.Random.Range(0.005f, 0.15f);
            yield return new WaitForSeconds(randomDelay);
            _wiggleTimer += randomDelay;
        }

        _falling = true;

        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach(var collider in colliders)
        {
            collider.enabled = false;
        }
 
        float fallTimer = 0;
        
        while (fallTimer < 3)
        {
            transform.position += Vector3.down * Time.deltaTime * _fallSpeed;
            fallTimer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_falling)
            return;

        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        _playersInTrigger.Remove(player);
        if (_playersInTrigger.Count == 0)
        {
            PlayerInside = false;
            StopCoroutine(WiggleAndFall());

            if (_resetOnEmpty)
                _wiggleTimer = 0;
        }
    }
}
