using System;
using System.Collections.Generic;
using UnityEngine;
public class Collectible : MonoBehaviour
{
    //List<Collector> _collectors = new List<Collector>();
    public event Action OnPickedUp;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        OnPickedUp?.Invoke();

        var audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
            audioSource.Play();
        /*foreach(var collector in _collectors)
        {
            collector.ItemPickedUp();
        }*/
        
    }

    /*public void AddCollector(Collector collector)
    {
        _collectors.Add(collector);
    }*/
}