using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]float _speed = 1;
    [SerializeField] float _jumpForce = 200;
    [SerializeField] float _fastFallForce = 200;
    [SerializeField] int _maxJumps = 2;
    [SerializeField] Transform _feet;

    Vector3 _startPosition;
    int _jumpsRemaining;
    

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
        _jumpsRemaining = _maxJumps;
    }

    // Update is called once per frame
    void Update()
    {
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
            rigidbody2D.AddForce(Vector2.up * _jumpForce);
            _jumpsRemaining--;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            rigidbody2D.AddForce(Vector2.up * -_jumpForce);
            rigidbody2D.AddForce(Vector2.down * _fastFallForce);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var hit = Physics2D.OverlapCircle(_feet.position, 0.1f, LayerMask.GetMask("Ground"));
        if (hit != null)
        {
            _jumpsRemaining = _maxJumps;
        }
    }

    internal void ResetToStart()
    {
        transform.position = _startPosition;
    }
}
