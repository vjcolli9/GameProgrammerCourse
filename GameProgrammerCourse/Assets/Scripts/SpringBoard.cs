using UnityEngine;

public class SpringBoard : MonoBehaviour
{

    [SerializeField] float _bounceVelocity = 10f;
    [SerializeField] Sprite _downSprite;

    SpriteRenderer _spriteRenderer;
    Sprite _upSprite;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _upSprite = _spriteRenderer.sprite;
    }
  
    

    void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            var rigidbody2D = player.GetComponent<Rigidbody2D>();
            if (rigidbody2D != null)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, _bounceVelocity);
                _spriteRenderer.sprite = _downSprite;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            _spriteRenderer.sprite = _upSprite;
        }
    }
}
