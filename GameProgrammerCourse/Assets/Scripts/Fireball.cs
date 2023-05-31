using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float _launchForce = 5;
    [SerializeField] float _bounceForce = 5f;
    int _bouncesRemaining = 3;
    Rigidbody2D _rigidbody;

    public float Direction { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = new Vector2(_launchForce * Direction, _bounceForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ITakeDamage damageable = collision.collider.GetComponent<ITakeDamage>();
        if (damageable != null) 
        {
            damageable.TakeDamage();
            Destroy(gameObject);
            return;
        }

        _bouncesRemaining--;
        if (_bouncesRemaining < 0)
            Destroy(gameObject);
        else
            _rigidbody.velocity = new Vector2(_launchForce * Direction, _bounceForce);
    }
}
