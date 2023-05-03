using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartLevelButton : MonoBehaviour
{
    [SerializeField] string _levelName;

    public void LoadLevel()
    {
        SceneManager.LoadScene(_levelName);
    }

    private void OnValidate()
    {
        //Question mark runs a null reference check
        GetComponentInChildren<TMP_Text>()?.SetText(_levelName);
    }
}
