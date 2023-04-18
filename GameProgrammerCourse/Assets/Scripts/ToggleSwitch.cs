using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : MonoBehaviour
{
    [SerializeField] Sprite _leftSprite;
    [SerializeField] Sprite _rightSprite;

    SpriteRenderer _spriteRenderer;
    Sprite _midSprite;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _midSprite = _spriteRenderer.sprite;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player == null)
            return;

        var playerRigidBody = player.GetComponent<Rigidbody2D>();
        if (playerRigidBody == null)
            return;

        bool wasOnRight = collision.transform.position.x > transform.position.x;
        bool playerWalkingRight = playerRigidBody.velocity.x > 0;
        bool playerWalkingLeft = playerRigidBody.velocity.x < 0;

        //_spriteRenderer.sprite = wasOnRight && playerWalkingRight ? _rightSprite : _leftSprite;
        if (wasOnRight && playerWalkingRight)
            _spriteRenderer.sprite = _rightSprite;
        else if (wasOnRight == false && playerWalkingLeft)
            _spriteRenderer.sprite = _leftSprite;
    }
}
