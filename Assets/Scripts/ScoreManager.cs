using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;
    public Text scoreText;
    public TMP_Text tmpScoreText;

    public TMP_Text finalScore;

    void Awake()
    {
        instance = this;
    }

    public void AddScore(int amount)
    {
        score += amount;
        //scoreText.text = "Score: " + score;
        tmpScoreText.text = "Score:" + score; // Update the TextMeshPro text
        finalScore.text = "Your score: " + score; // Update the final score text
    }
}