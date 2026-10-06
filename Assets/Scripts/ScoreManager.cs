using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;

    public TMP_Text tmpScoreText;

    public TMP_Text finalScore;

    private void Start()
    {
        score = 0;
        UpdateScoreText();
    }

    void Awake()
    {
        instance = this;
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        tmpScoreText.text = "Score:=" + score;
        finalScore.text = "Your score: " + score;
    }
}
