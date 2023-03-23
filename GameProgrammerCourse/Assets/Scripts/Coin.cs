using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    static int _coinsCollected;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        gameObject.SetActive(false);
        _coinsCollected++;
        Debug.Log(_coinsCollected);
    }
}
