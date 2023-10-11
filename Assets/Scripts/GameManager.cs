using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score;

    public Text scoreText;
    public Text timeText;

    public GameObject playButton;
    public GameObject gameOver;
    public GameObject spawner;
    public GameObject powerUp;

    public Sprite gameOverSprite;
    public Sprite getReadySprite;


    public Player player;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Play();
    }

    public void Update()
    {
        timeText.text = Time.timeSinceLevelLoad.ToString();
    }

    public void Play()
    {
        DiableScoringSystem();
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
        EnablePowerUpSystem();
    }

    public void Pause()
    {
        DiablePowerUpSystem();
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

    private void EnableScoringSystem()
    {
        scoreText.gameObject.SetActive(true);
        score = 0;
        scoreText.text = score.ToString();
    }

    private void EnablePowerUpSystem()
    {
        powerUp.gameObject.SetActive(true);
    }

    private void DiableScoringSystem()
    {
        scoreText.gameObject.SetActive(false);
    }

    private void DiablePowerUpSystem()
    {
        powerUp.gameObject.SetActive(false);
    }

    public void EnableSpawner()
    {
        spawner.SetActive(true);
    }

    public void DisableSpawner()
    {
        spawner.SetActive(false);
    }

    public void ReverseGravity()
    {
        player.ReverseGravity();
    }
    
}
