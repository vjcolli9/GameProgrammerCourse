using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleSwitch : MonoBehaviour
{
    [SerializeField] UnityEvent _onLeft;
    [SerializeField] UnityEvent _onRight;
    [SerializeField] UnityEvent _onCenter;

    [SerializeField] Sprite _leftSprite;
    [SerializeField] Sprite _rightSprite;
    [SerializeField] Sprite _centerSprite;

    SpriteRenderer _spriteRenderer;
    ToggleDirection _currentDirection;
    Sprite _midSprite;

    enum ToggleDirection
    {
        Left,
        Center,
        Right,
    }
    
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
        {
            SetToggleDirection(ToggleDirection.Right);
        }
        else if (wasOnRight == false && playerWalkingLeft)
        {
            SetToggleDirection(ToggleDirection.Left);
        }
    }

    void SetToggleDirection(ToggleDirection direction)
    {
        if (_currentDirection == direction)
            return;
        _currentDirection = direction;
        switch (direction)
        {
            case ToggleDirection.Left:
                _spriteRenderer.sprite = _leftSprite;
                _onLeft.Invoke();
                break;
            case ToggleDirection.Center:
                _spriteRenderer.sprite = _centerSprite;
                _onCenter.Invoke();
                break;
            case ToggleDirection.Right:
                _spriteRenderer.sprite = _rightSprite;
                _onRight.Invoke();
                break;
            default:
                break;
        }     
    }

    public void LogUsingEvent()
    {
        Debug.Log("Using Event");
    }
}
