using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float _launchForce = 5;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(_launchForce, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
