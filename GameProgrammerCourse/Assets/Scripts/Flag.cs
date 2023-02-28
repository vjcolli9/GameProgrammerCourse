using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player == null)
            return;

        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Raise");
        //play flag wave
        //load a new level
    }
}
