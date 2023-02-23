using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]float _speed = 1;
    [SerializeField] float _jumpForce = 200;
    [SerializeField] float _fastFallForce = 200;

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
        
        if (Input.GetButtonDown("Fire1"))
        {
            rigidbody2D.AddForce(Vector2.up * _jumpForce);
        }

        /*if (Input.GetButtonDown("Fire2"))
        {
            rigidbody2D.AddForce(Vector2.up * -_jumpForce);
            rigidbody2D.AddForce(Vector2.down * _fastFallForce);
        }*/
    }
}
