﻿using System;
using UnityEngine;

public class HittableFromBelow : MonoBehaviour
{
    [SerializeField] protected Sprite _usedSprite;
    Animator _animator;
    AudioSource _audioSource;

    protected virtual bool CanUse => true;
    //protected virtual bool CanUse { get; } = true;
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player == null)
            return;

        if (collision.contacts[0].normal.y > 0)
        {
            if(CanUse == true)
                PlayAnimation();
            if(CanUse == true)
                GetComponent<AudioSource>().Play();
            Use();
            if (CanUse == false)
            {
                GetComponent<SpriteRenderer>().sprite = _usedSprite;
            }
        }
    }

    void PlayAnimation()
    {
        if (_animator != null)
            _animator.SetTrigger("Use");
    }

    protected virtual void Use()
    {
        Debug.Log($"Used {gameObject.name}");
    }
}
