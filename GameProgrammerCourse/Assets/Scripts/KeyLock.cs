using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyLock : MonoBehaviour
{
    [SerializeField] UnityEvent _onUnlocked;

    // Start is called before the first frame update
    public void Unlock()
    {
        Debug.Log("Unlocked");
        _onUnlocked.Invoke();
    }

}
