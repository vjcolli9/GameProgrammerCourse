using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] Transform _leftSensor;
    [SerializeField] Transform _rightSensor;
    [SerializeField] Sprite _deadSprite;
    Rigidbody2D _rigidbody2D;
    float _direction = -1f;
    

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.velocity = new Vector2(_direction, _rigidbody2D.velocity.y);
        if (_direction < 0)
        {
            ScanSensor(_leftSensor);
        }
        else
        {
            ScanSensor(_rightSensor);
        }
        
    }

    private void ScanSensor(Transform sensor)
    {
        Debug.DrawRay(sensor.position, Vector2.down * 0.1f, Color.red);
        RaycastHit2D result = Physics2D.Raycast(sensor.position, Vector2.down, 0.1f);
        if (result.collider == null)
            TurnAround();

        Debug.DrawRay(sensor.position, new Vector2(_direction, 0) * 0.1f, Color.red);
        RaycastHit2D sideResult = Physics2D.Raycast(sensor.position, new Vector2(_direction, 0), 0.1f);
        if (sideResult.collider != null)
            TurnAround();
    }

    void TurnAround()
    {
        _direction *= -1;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = _direction > 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        if (player == null)
            return;

        Vector2 normal = collision.contacts[0].normal;
        if (normal.y <= -0.5)
        {
            StartCoroutine(Die());
        }
        else
        {
            player.ResetToStart();
        }
        //Debug.Log($"Normal = {normal}");


    }

    IEnumerator Die()
    {

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _deadSprite;
        GetComponent<Animator>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;

        float alpha = 1f;
        while(alpha > 0)
        {
            yield return null;
            alpha -= Time.deltaTime;
            spriteRenderer.color = new Color(1, 1, 1, alpha);

        }
        
        spriteRenderer.color = new Color();
        //Destroy(gameObject);
    }
}
