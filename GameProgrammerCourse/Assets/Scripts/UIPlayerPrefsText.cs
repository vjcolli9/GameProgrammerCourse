using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerPrefsText : MonoBehaviour
{
    [SerializeField] string _key;

    // Start is called before the first frame update
    void OnEnable()
    {
        int value = PlayerPrefs.GetInt(_key);
        GetComponent<TMP_Text>().SetText(value.ToString());
    }

    [ContextMenu("Clear HighScore")]
    void ClearLevelUnlocked()
    {
        var startButton = GetComponent<UIStartLevelButton>();
        string key = _key;
        PlayerPrefs.DeleteKey(key);
    }
}
