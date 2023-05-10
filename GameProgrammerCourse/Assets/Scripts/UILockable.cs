using UnityEngine;

public class UILockable : MonoBehaviour
{
    private void OnEnable()
    {
        var startButton = GetComponent<UIStartLevelButton>();
        string key = startButton.LevelName + "Unlocked";
        int unlocked = PlayerPrefs.GetInt(key, 0);

        if (unlocked == 0)
        {
            gameObject.SetActive(false);
        }
    }

    [ContextMenu("Clear Unlocked")]
    void ClearLevelUnlocked()
    {
        var startButton = GetComponent<UIStartLevelButton>();
        string key = startButton.LevelName + "Unlocked";
        PlayerPrefs.DeleteKey(key);
    }
}

