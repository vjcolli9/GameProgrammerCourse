using UnityEngine;

public class ItemBox : HittableFromBelow
{
    [SerializeField] GameObject _itemPrefab;
    [SerializeField] GameObject _item;
    [SerializeField] Vector2 _itemLaunchVelocity;
    bool _used;

    private void Start()
    {
        if(_item != null)
        _item.SetActive(false);
    }

    protected override bool CanUse => _used == false;
    protected override void Use()
    {
        Debug.Log("Used Itembox");
        _item = Instantiate(_itemPrefab, transform.position + Vector3.up, Quaternion.identity, transform);
        if (_item == null)
            return;

        base.Use();

        _used = true;
        _item.SetActive(true);
        var itemRigidbody = _item.GetComponent<Rigidbody2D>();
        if (itemRigidbody != null)
        {
            
            itemRigidbody.velocity = _itemLaunchVelocity;
        }
    }
}
