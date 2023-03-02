using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]float _speed = 1;
    [SerializeField] float _jumpVelocity = 10;
    [SerializeField] float _fastFallForce = 1;
    [SerializeField] int _maxJumps = 2;
    [SerializeField] Transform _feet;
    [SerializeField] float _downPull = 0.1f;
    [SerializeField] float _fastFallTimer = 0.45f;
    [SerializeField] float _maxJumpDuration = 0.1f;

    Vector3 _startPosition;
    int _jumpsRemaining;
    bool _canFastFall = false;
    float _fallTimer;
    float _jumpTimer = 0;
    float _downForce = 0;
    Rigidbody2D _rigidbody2D;
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    float _horizontal;
    private bool _isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
        _jumpsRemaining = _maxJumps;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIsGrounded();

        ReadHorizontalInput();
        MoveHorizontal();

        UpdateAnimator();
        UpdateSpriteDirection();

        if (ShouldStartJump())
            Jump();
        else if (ShouldContinueJump())
            ContinueJump();

        if (_jumpTimer < 100f)
            _jumpTimer += Time.deltaTime;

        if (_isGrounded && _fallTimer > 0)
        {
            _fallTimer = 0;
            _jumpsRemaining = _maxJumps;
            _downForce = 0;

        }
        else
        {
            if (_fallTimer < 100f)
                _fallTimer += Time.deltaTime;

            if (_downForce < 100f)
                _downForce = _downPull * _fallTimer * _fallTimer;

            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, (_rigidbody2D.velocity.y - _downForce));

            //fastfall
            if (Input.GetButtonDown("Fire2") && _fallTimer >= _fastFallTimer && _canFastFall == true)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, (_rigidbody2D.velocity.y - _fastFallForce));
                _canFastFall = false;
            }
        }


    }

    private void ContinueJump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
        _fallTimer = 0;
    }

    private bool ShouldContinueJump()
    {
        return Input.GetButton("Fire1") && _jumpTimer <= _maxJumpDuration;
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
        _jumpsRemaining--;
        _fallTimer = 0;
        _jumpTimer = 0;
        _canFastFall = true;
    }

    private bool ShouldStartJump()
    {
        return Input.GetButtonDown("Fire1") && _jumpsRemaining > 0;
    }

    private void MoveHorizontal()
    {
        if (Mathf.Abs(_horizontal) >= 1)
        {
            _rigidbody2D.velocity = new Vector2(_horizontal, _rigidbody2D.velocity.y);
        }
    }

    private void ReadHorizontalInput()
    {
        _horizontal = Input.GetAxis("Horizontal") * _speed;
    }

    private void UpdateSpriteDirection()
    {
        if (_horizontal != 0)
        {
            _spriteRenderer.flipX = (_horizontal < 0);
        }
    }

    void UpdateAnimator()
    {
        bool walking = _horizontal != 0;
        _animator.SetBool("Walk", walking);
    }

    void UpdateIsGrounded()
    {
        var hit = Physics2D.OverlapCircle(_feet.position, 0.1f, LayerMask.GetMask("Ground"));
        _isGrounded = hit != null;
    }

    internal void ResetToStart()
    {
        transform.position = _startPosition;
    }
}
