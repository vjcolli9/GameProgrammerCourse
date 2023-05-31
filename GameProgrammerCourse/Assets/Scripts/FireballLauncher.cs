using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballLauncher : MonoBehaviour
{
    [SerializeField] Fireball _fireballPrefab;
    [SerializeField] int _playerNumber = 1;
    Player _player;
    string _fireButton;

    // Start is called before the first frame update
    void Awake()
    {
        _player = GetComponent<Player>();
        _fireButton = $"P{_player.PlayerNumber}Fire";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(_fireButton))
        {
            Instantiate(_fireballPrefab, transform.position, Quaternion.identity);
        }
    }
}
