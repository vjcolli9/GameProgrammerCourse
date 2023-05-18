using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int CoinsCollected;

    [SerializeField] List<AudioClip> _clips;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;
        if(_clips.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, _clips.Count);
            AudioClip clip = _clips[randomIndex];
            GetComponent<AudioSource>().PlayOneShot(clip);
        }  
        else
        {
            GetComponent<AudioSource>().Play();
        }
        
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        CoinsCollected++;
        Debug.Log(CoinsCollected);

        ScoreSystem.Add(100);
    }
}
