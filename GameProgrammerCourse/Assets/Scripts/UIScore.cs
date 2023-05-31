using System;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        ScoreSystem.OnScoreChanged += UpdateScoreText;
        UpdateScoreText(ScoreSystem.Score);
    }

    private void UpdateScoreText(object score)
    {
        throw new NotImplementedException();
    }

    void OnDestroy()
    {
        ScoreSystem.OnScoreChanged -= UpdateScoreText;
    }

    // Update is called once per frame
    void UpdateScoreText(int score)
    {
        Debug.Log("Updating Score to " + score);
        _text.SetText(score.ToString());
    }
}
