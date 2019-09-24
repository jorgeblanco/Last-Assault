using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    private int _score;
    private TextMeshProUGUI _scoreText;

    public void ScoreHit(int value = 100)
    {
        _score += value;
        UpdateScore();
    }

    private void Start()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
        UpdateScore();
    }

    private void UpdateScore()
    {
        _scoreText.text = "Score: " + _score;
    }
}
