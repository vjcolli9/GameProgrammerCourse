using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public bool PlayerInside;
    Coroutine _coroutine;
    HashSet<Player> _playersInTrigger = new HashSet<Player>();
    Vector3 _initialPosition;
    [SerializeField] float _fallSpeed = 9;
    bool _falling;

    void Start()
    {
        _initialPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        _playersInTrigger.Add(player);
        PlayerInside = true;

        if(_playersInTrigger.Count == 1)
            _coroutine = StartCoroutine(WiggleAndFall());
    }

    IEnumerator WiggleAndFall()
    {
        Debug.Log("Waiting to wiggle");
        yield return new WaitForSeconds(0.25f);
        Debug.Log("Wiggling");
        float wiggleTimer = 0f;
        while (wiggleTimer < 1f)
        {
            float randomX = UnityEngine.Random.Range(-0.05f, 0.01f);
            float randomY = UnityEngine.Random.Range(-0.05f, 0.01f);
            transform.position = _initialPosition + new Vector3(randomX, randomY);
            float randomDelay = UnityEngine.Random.Range(0.005f, 0.01f);
            yield return new WaitForSeconds(randomDelay);
            wiggleTimer += randomDelay;
        }

        Debug.Log("Falling");
        _falling = true;
        //Collider2D[] colliders = GetComponents<Collider2D>();
        foreach(var collider in GetComponents<Collider2D>())
        {
            collider.enabled = false;
        }
        float fallTimer = 0;

        while(fallTimer < 3f)
        {
            transform.position += Vector3.down * Time.deltaTime * _fallSpeed;
            fallTimer += Time.deltaTime;
            Debug.Log(fallTimer);
            yield return null;
        }

        Destroy(gameObject);
        //yield return new WaitForSeconds(1f);
        //Debug.Log("Falling");
        //yield return new WaitForSeconds(3f);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(_falling)
        {
            return;
        }

        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        _playersInTrigger.Remove(player);
        
        if(_playersInTrigger.Count == 0)
        {
            PlayerInside = false;
            StopCoroutine(_coroutine);
        }
    }
}
