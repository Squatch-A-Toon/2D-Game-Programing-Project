using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButtonSwitch : MonoBehaviour
{
    [SerializeField] Sprite _pressedSprite;
    [SerializeField] UnityEvent _onPressed;
    [SerializeField] UnityEvent _onReleased;
    [SerializeField] int _playerNumber;
    SpriteRenderer _spriteRenderer;
    Sprite _releasedSprite;

    void Awake()
    {
        //cache Sprite renderer Component
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //cache the sprite for Released Sprite
        _releasedSprite = _spriteRenderer.sprite;
        BecomeReleased();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //get a reference for the player
        var player = collision.GetComponent<Player>();
        //if there is no player or a players player number dosn't exist
        if (player == null  || player.PlayerNumber != _playerNumber)
            //do nothing
            return;
        BecomePressed();
    }

    void BecomePressed()
    {
        //set the sprite to the pressed sprite
        _spriteRenderer.sprite = _pressedSprite;
        _onPressed?.Invoke();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //Get Referance to player
        var player = collision.GetComponent<Player>();
        //if therer is no player
        if (player == null || player.PlayerNumber != _playerNumber)
            //do nothing
            return;

        BecomeReleased();
    }

    void BecomeReleased()
    {
        if(_onReleased.GetPersistentEventCount() != 0)
        {
            //set the sprite to the released sprite
            _spriteRenderer.sprite = _releasedSprite;
            _onReleased?.Invoke();
        }
    }
}
