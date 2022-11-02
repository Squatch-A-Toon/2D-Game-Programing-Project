using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleSwitch : MonoBehaviour
{
    [SerializeField] ToggleDirection _startingDirection = ToggleDirection.Center;

    [SerializeField] UnityEvent _onRight;
    [SerializeField] UnityEvent _onLeft;
    [SerializeField] UnityEvent _onCenter;

    public Sprite rightSprite;
    public Sprite leftSprite;
    public Sprite centerSprite;
    private SpriteRenderer _spriteRenderer;
    ToggleDirection _currentDirection;

    enum ToggleDirection
    {
        Left,
        Center,
        Right,
    }
    // Start is called before the first frame update
    void Awake()
    {
        //get sprite Renderer component
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetToggleDirection(_startingDirection, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Get player component
        var _player = collision.GetComponent<Player>();
        //if there is no collision with a player
        if (_player == null)
            //Do nothing
            return;
        //Get Rigidbody Component on player
        var playerRigidbody = _player.GetComponent<Rigidbody2D>();
        //if there is no rigidbody on the player
        if (playerRigidbody == null)
            //do nothing
            return;
        //check to see if the collision positiopn is greater than the objects position
        bool wasOnRight = collision.transform.position.x > transform.position.x;
        //check to see if teh player was wal;king right
        bool playerWalkingright = playerRigidbody.velocity.x > 0;
        //check to see if teh player was wal;king left
        bool playerWalkingleft = playerRigidbody.velocity.x < 0;

        //geet bool value by finding if our transform is greater than other transform
        bool WasOnright = collision.transform.position.x > transform.position.x;
        //change the sprite to left sprite if was on right = true, change to right sprite if was on right = false
        if (WasOnright && playerWalkingright)
        {
            SetToggleDirection(ToggleDirection.Right, false);
        }
            
        else if (wasOnRight == false && playerWalkingleft)
        {
            SetToggleDirection(ToggleDirection.Left, false);
        }
          
    }

    void SetToggleDirection(ToggleDirection direction, bool force)
    {
        if (force == false && _currentDirection == direction)
            return;

        _currentDirection = direction;

        switch (direction)
        {
            case ToggleDirection.Left:
                _spriteRenderer.sprite = leftSprite;
                _onLeft.Invoke();
                break;
            case ToggleDirection.Center:
                _spriteRenderer.sprite = centerSprite;
                _onCenter.Invoke();
                break;
            case ToggleDirection.Right:
                _spriteRenderer.sprite = rightSprite;
                _onRight.Invoke();
                break;
            default:
                break;
        }
    }


}
