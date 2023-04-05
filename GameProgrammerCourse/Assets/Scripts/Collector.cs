using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] Collectible[] _collectibles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var collectible in _collectibles)
        {
            if (collectible.isActiveAndEnabled)
                return;
        }
        

        Debug.Log("Got All Gems");
    }
}
