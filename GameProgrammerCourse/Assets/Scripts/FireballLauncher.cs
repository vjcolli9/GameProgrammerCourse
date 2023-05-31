using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireballLauncher : MonoBehaviour
{
    [SerializeField] Fireball _fireballPrefab;
    [SerializeField] int _playerNumber = 1;
    [SerializeField] float _fireRate = 1f;

    Player _player;
    string _fireButton;
    string _horizontalAxis;
    float _nextFireTime;
    

    // Start is called before the first frame update
    void Awake()
    {
        _player = GetComponent<Player>();
        _fireButton = $"P{_player.PlayerNumber}Fire";
        _horizontalAxis = $"P{_player.PlayerNumber}Horizontal";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(_fireButton) && Time.time >= _nextFireTime)
        {
            var horizontal = Input.GetAxis(_horizontalAxis);
            Fireball fireball = Instantiate(_fireballPrefab, transform.position, Quaternion.identity);
            fireball.Direction = horizontal >= 0 ? 1f : -1f;
            _nextFireTime = Time.time + _fireRate;
        }
    }
}
