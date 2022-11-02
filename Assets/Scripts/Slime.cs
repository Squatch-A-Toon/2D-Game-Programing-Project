using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] Transform _sensorLeft;
    [SerializeField] Transform _sensorRight;
    [SerializeField] Sprite _deadSprite;

    private Rigidbody2D _Rigidbody2d;
    private float _direction = -1;


    // Start is called before the first frame update
    void Start()
    {
        _Rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _Rigidbody2d.velocity = new Vector2( _direction, _Rigidbody2d.velocity.y);

        if (_direction < 0)
        {
            SensorCheck(_sensorLeft);
        }
        else
        {
            SensorCheck(_sensorRight);
        }
    }

    private void SensorCheck(Transform sensor)
    {
        Debug.DrawRay(sensor.position, Vector2.down * 0.1f, Color.red);        
        var result = Physics2D.Raycast(sensor.position, Vector2.down, 0.1f);
        if (result.collider == null)
            TurnAround();

        Debug.DrawRay(sensor.position, new Vector2(_direction, 0) * 0.1f, Color.red);
        var sideResult = Physics2D.Raycast(sensor.position, new Vector2(_direction, 0), 0.1f);
        if (sideResult.collider != null)
            TurnAround();
    }

    private void TurnAround()
    {
        _direction *= -1;
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = _direction > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player == null)
            return;
        var contact = collision.contacts[0];
        Vector2 normal = contact.normal;

        if (normal.y <= -0.5)
            StartCoroutine(Die());
        else
            player.ResetToStart();
    }

    IEnumerator Die()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = _deadSprite;
        GetComponent<Animator>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        float alpha = 1;

        while (alpha > 0 )
        {
            yield return null;
            alpha -= Time.deltaTime;
            spriteRenderer.color = new Color(1, 1, 1, alpha);
        }        
    }
}
