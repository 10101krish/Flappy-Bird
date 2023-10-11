using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score;

    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;

    public Sprite gameOverSprite;
    public Sprite getReadySprite;


    public Player player;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Pause();
    }

    public void Play()
    {
        scoreText.gameObject.SetActive(false);

        StartCoroutine(DisplayGetReady());

        playButton.SetActive(false);
        player.enabled = true;
        DestroyAllPipes();
    }

    IEnumerator DisplayGetReady()
    {
        gameOver.GetComponent<Image>().sprite = getReadySprite;

        yield return new WaitForSecondsRealtime(1);
        gameOver.SetActive(false);
        Time.timeScale = 1;
        EnableScoringSystem();
    }

    private void EnableScoringSystem()
    {
        scoreText.gameObject.SetActive(true);
        score = 0;
        scoreText.text = score.ToString();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void GameOver()
    {
        gameOver.GetComponent<Image>().sprite = gameOverSprite;
        gameOver.SetActive(true);
        playButton.SetActive(true);
        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    private void DestroyAllPipes()
    {
        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for (int i = 0; i < pipes.Length; i++)
            Destroy(pipes[i].gameObject);
    }
}
