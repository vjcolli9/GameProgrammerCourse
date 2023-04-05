using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] List<Collectible> _collectibles;
    TMP_Text _remainingText;

    // Start is called before the first frame update
    void Start()
    {
        _remainingText = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int countRemaining = 0;
        foreach(var collectible in _collectibles)
        {
            if (collectible.isActiveAndEnabled)
                countRemaining++;
        }

        //catches null exceptions with '?'
        _remainingText?.SetText(countRemaining.ToString());

        if (countRemaining > 0)
            return;

        Debug.Log("Got All Gems");
    }

    void OnValidate()
    {
        _collectibles = _collectibles.Distinct().ToList();
    }
}
