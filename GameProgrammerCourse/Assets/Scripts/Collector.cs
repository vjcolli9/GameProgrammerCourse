using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
    [SerializeField] List<Collectible> _collectibles;
    [SerializeField] UnityEvent _onCollectionComplete;

    TMP_Text _remainingText;
    int _countCollected = 0;

    // Start is called before the first frame update
    void Start()
    {
        _remainingText = GetComponentInChildren<TMP_Text>();
        foreach (var collectible in _collectibles)
        {
            collectible.SetCollector(this);
        }
        int countRemaining = _collectibles.Count - _countCollected;

        //catches null exceptions with '?'
        _remainingText?.SetText(countRemaining.ToString());
    }

    // Update is called once per frame
    public void ItemPickedUp()
    {
        _countCollected++;
        int countRemaining = _collectibles.Count - _countCollected;

        //catches null exceptions with '?'
        _remainingText?.SetText(countRemaining.ToString());

        if (countRemaining > 0)
            return;

        Debug.Log("Got All Gems");
        _onCollectionComplete.Invoke();
    }

    void OnValidate()
    {
        _collectibles = _collectibles.Distinct().ToList();
    }
}
