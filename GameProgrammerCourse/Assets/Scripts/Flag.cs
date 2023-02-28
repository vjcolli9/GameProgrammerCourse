using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    [SerializeField] string _sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player == null)
            return;

        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Raise");

        SceneManager.LoadScene(_sceneName);
        //load a new level
    }
}
