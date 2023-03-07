using UnityEngine;
using UnityEngine.Events;

public class PushButtonSwitch : MonoBehaviour
{
    [SerializeField] Sprite _pressedSprite;
    [SerializeField] UnityEvent _onPressed;
    [SerializeField] UnityEvent _onReleased;

    SpriteRenderer _spriteRenderer;
    Sprite _releasedSprite;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _releasedSprite = _spriteRenderer.sprite;
        BecomeReleased();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player == null)
            return;
        BecomePressed();
    }

    void BecomePressed()
    {
        _spriteRenderer.sprite = _pressedSprite;
        _onPressed?.Invoke();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player == null)
            return;
        BecomeReleased();
    }

    void BecomeReleased()
    {
        _spriteRenderer.sprite = _releasedSprite;
        _onReleased?.Invoke();
    }
}
