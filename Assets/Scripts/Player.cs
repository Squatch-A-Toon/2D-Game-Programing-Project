using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] int _playerNumber = 1;
    [Header("Movement")]
    [SerializeField] float _speed = 4f;
    [SerializeField] float _slipFactor;
    [Header("Jump")]
    [SerializeField] float _jumpVelocity = 10f;
    [SerializeField] int _maxJumps = 2;
    [SerializeField] Transform _feet;
    [SerializeField] float _downPull = 5f;
    [SerializeField] float _maxJumpDuration = 0.1f;
    
    Vector2 _startingPosition;
    int _jumpsremaining;
    float _fallTimer;
    private float _jumpTimer;
    Rigidbody2D _rigidbody2D;
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    float _horizontal;
    bool _isGrounded;
    bool _isOnSlipperySurface;
    string _jumpButton;
    string _horizontalaxis;
    int _layerMask;

    public int PlayerNumber => _playerNumber;

    // Start is called before the first frame update
    void Start()
    {
        _startingPosition = transform.position;
        _jumpsremaining = _maxJumps;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _jumpButton = $"P{_playerNumber}Jump";
        _horizontalaxis = $"P{_playerNumber}Horizontal";
        _layerMask = LayerMask.GetMask("Default");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIsGrounded();
        ReadHorizontalInput();

        if (_isOnSlipperySurface)
            SlipHorizontal();
        else
            MoveHorizontal();

        UpdateAnimator();

        UpdateSpriteDirection();

        if (ShouldJump())
        {
            Jump();
        }
        else if (ShouldContinueJump())
        {
            ContinueJump();
        }

        _jumpTimer += Time.deltaTime;

        if (_isGrounded && _fallTimer > 0)
        {
            _fallTimer = 0;
            _jumpsremaining = _maxJumps;
        }
        else
        {
            _fallTimer += Time.deltaTime;
            var downForce = _downPull * _fallTimer * _fallTimer;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y - downForce);
        }
    }

    private void ContinueJump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
        _fallTimer = 0;
    }

    private bool ShouldContinueJump()
    {
        return Input.GetButton(_jumpButton) && _jumpTimer <= _maxJumpDuration;
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
        _jumpsremaining--;
        _fallTimer = 0;
        _jumpTimer = 0;
    }

    private bool ShouldJump()
    {
        return Input.GetButtonDown(_jumpButton) && _jumpsremaining > 0;
    }

    private void MoveHorizontal()
    {
        _rigidbody2D.velocity = new Vector2(_horizontal, _rigidbody2D.velocity.y);
    }

    private void SlipHorizontal()
    {
        var desiredVelocity = new Vector2(_horizontal * _speed, _rigidbody2D.velocity.y);
        var smoothedVelocity = Vector2.Lerp(_rigidbody2D.velocity, desiredVelocity, Time.deltaTime / _slipFactor);
        _rigidbody2D.velocity = smoothedVelocity;
    }
    

    private void ReadHorizontalInput()
    {
        _horizontal = Input.GetAxis(_horizontalaxis) * _speed;
    }

    private void UpdateSpriteDirection()
    {
        if (_horizontal != 0)
        {
            _spriteRenderer.flipX = _horizontal < 0;
        }
    }

    private void UpdateAnimator()
    {
        bool walking = _horizontal != 0;
        _animator.SetBool("Walk", walking);
        _animator.SetBool("Jump", ShouldContinueJump());
    }

    void UpdateIsGrounded()
    {
        var hit = Physics2D.OverlapCircle(_feet.position, 0.1f, _layerMask);
        _isGrounded = hit != null;

        if (hit != null)
            _isOnSlipperySurface = hit.CompareTag("Slippery");
        else
            _isOnSlipperySurface = false;
    }

    internal void ResetToStart()
    {
        _rigidbody2D.position = _startingPosition;
    }

    internal void TeleportTo(Vector3 position)
    {
        _rigidbody2D.position = position;
        _rigidbody2D.velocity = Vector2.zero;
    }
}
