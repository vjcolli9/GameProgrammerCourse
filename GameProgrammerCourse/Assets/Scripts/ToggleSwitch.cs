using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleSwitch : MonoBehaviour
{
    [SerializeField] ToggleDirection _startingDirection = ToggleDirection.Center;
    [SerializeField] UnityEvent _onLeft;
    [SerializeField] UnityEvent _onRight;
    [SerializeField] UnityEvent _onCenter;

    [SerializeField] Sprite _leftSprite;
    [SerializeField] Sprite _rightSprite;
    [SerializeField] Sprite _centerSprite;

    SpriteRenderer _spriteRenderer;
    ToggleDirection _currentDirection;

    enum ToggleDirection
    {
        Left,
        Center,
        Right,
    }
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetToggleDirection(_startingDirection, true);
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


        if (wasOnRight && playerWalkingRight)
        {
            SetToggleDirection(ToggleDirection.Right);
        }
        else if (wasOnRight == false && playerWalkingLeft)
        {
            SetToggleDirection(ToggleDirection.Left);
        }
    }

    void SetToggleDirection(ToggleDirection direction, bool force = false)
    {

        if (force == false && _currentDirection == direction)
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

    void OnValidate()
    {
        if (_startingDirection == ToggleDirection.Left)
            GetComponent<SpriteRenderer>().sprite = _leftSprite;
        else if (_startingDirection == ToggleDirection.Right)
            GetComponent<SpriteRenderer>().sprite = _rightSprite;
        else if (_startingDirection == ToggleDirection.Center)
            GetComponent<SpriteRenderer>().sprite = _centerSprite;

    }

    public void LogUsingEvent()
    {
        Debug.Log("Using Event");
    }
}
