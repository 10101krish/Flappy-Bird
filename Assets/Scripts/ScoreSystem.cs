using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;

    private int score;
    private int highScore;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("score", 0);
        highScoreText.gameObject.SetActive(false);
        UpdateScoreText(0);
        scoreText.text = "";
    }

    public void ResetScore()
    {
        UpdateScoreText(0);
        scoreText.color = Color.white;
    }

    public void IncreaseScore(int increase)
    {
        UpdateScoreText(score + increase);
        if (score > highScore)
            HighlightHighScore();
    }

    private void UpdateScoreText(int newScore)
    {
        score = newScore;
        scoreText.text = score.ToString();
    }

    public void HighlightHighScore()
    {
        scoreText.color = Color.yellow;
        highScoreText.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt("score", score);
            highScore = score;
        }
    }

    public void EnableScoringSystem()
    {
        scoreText.gameObject.SetActive(true);
        ResetScore();
    }

    public void DisableScoringSystem()
    {
        scoreText.gameObject.SetActive(false);
    }
}
