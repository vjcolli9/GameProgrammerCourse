using System;
using UnityEngine;

public class HittableFromBelow : MonoBehaviour
{
    [SerializeField] protected Sprite _usedSprite;
    
    protected virtual bool CanUse => true;
    //protected virtual bool CanUse { get; } = true;

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player == null)
            return;

        if (collision.contacts[0].normal.y > 0)
        {
            Use();
            if (CanUse == false)
            {
                GetComponent<SpriteRenderer>().sprite = _usedSprite;
            }
        }
    }

    protected virtual void Use()
    {
        Debug.Log($"Used {gameObject.name}");
    }
}
