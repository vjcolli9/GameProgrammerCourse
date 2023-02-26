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

    Vector3 _startPosition;
    int _jumpsRemaining;
    bool _canFastFall = false;
    float _fallTimer;


    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
        _jumpsRemaining = _maxJumps;
    }

    // Update is called once per frame
    void Update()
    {
        var hit = Physics2D.OverlapCircle(_feet.position, 0.1f, LayerMask.GetMask("Ground"));
        bool isGrounded = hit != null;
        var horizontal = Input.GetAxis("Horizontal") * _speed;
        var rigidbody2D = GetComponent<Rigidbody2D>();
        if(Mathf.Abs(horizontal) >= 1)
        {
            rigidbody2D.velocity = new Vector2(horizontal, rigidbody2D.velocity.y);
            Debug.Log($"Velocity = {rigidbody2D.velocity}");
        }
        

        var animator = GetComponent<Animator>();
        bool walking = horizontal != 0;
        animator.SetBool("Walk", walking);

        if (horizontal != 0)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = (horizontal < 0);
        }
        
        if (Input.GetButtonDown("Fire1") && _jumpsRemaining > 0)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, _jumpVelocity);
            _jumpsRemaining--;
            _fallTimer = 0;
            _canFastFall = true;
        }

        if(isGrounded)
        {
            _fallTimer = 0;
            _jumpsRemaining = _maxJumps;

        }
        else
        {
            _fallTimer += Time.deltaTime;
            var downForce = _downPull * _fallTimer * _fallTimer;
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, (rigidbody2D.velocity.y - downForce));

            //fastfall
            if (Input.GetButtonDown("Fire2") && _fallTimer >= _fastFallTimer && _canFastFall == true)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, (rigidbody2D.velocity.y - _fastFallForce));
                _canFastFall = false;
            }
        }

        
    }

    internal void ResetToStart()
    {
        transform.position = _startPosition;
    }
}
