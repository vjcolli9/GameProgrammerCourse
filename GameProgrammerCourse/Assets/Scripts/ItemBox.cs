using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField] Sprite _usedSprite;
    [SerializeField] GameObject _item;
    [SerializeField] Vector2 _itemLaunchVelocity;
    bool _used;

    private void Start()
    {
        if(_item != null)
        _item.SetActive(false);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_used)
            return;

        var player = collision.collider.GetComponent<Player>();
        if (player == null)
            return;

        if (collision.contacts[0].normal.y > 0)
        {
            GetComponent<SpriteRenderer>().sprite = _usedSprite;
            if (_item != null)
            {
                _used = true;
                _item.SetActive(true);
                var itemRigidbody = _item.GetComponent<Rigidbody2D>();
                if(itemRigidbody != null)
                {
                    itemRigidbody.velocity = _itemLaunchVelocity;
                }
            }
                
        }

    }
}
