using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score;

    public Text scoreText;
    public Text powerUpText;

    public GameObject playButton;
    public GameObject gameOver;
    public GameObject spawner;
    public GameObject powerUpSpawner;

    public Sprite gameOverSprite;
    public Sprite getReadySprite;

    public Player player;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Play();
    }

    public void Play()
    {
        powerUpText.text = "";
        DisableScoringSystem();
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
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void GameOver()
    {
        DisablePowerUpSystem();
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
        foreach (var pipe in pipes)
        {
            Destroy(pipe.gameObject);
        }
    }

    private bool DestroyAllPowerUps()
    {
        PowerUps[] powerUps = FindObjectsOfType<PowerUps>();
        foreach (var powerUp in powerUps)
        {
            Destroy(powerUp.gameObject);
        }

        return true;
    }

    private void EnableScoringSystem()
    {
        scoreText.gameObject.SetActive(true);
        score = 0;
        scoreText.text = score.ToString();
    }

    private void EnablePowerUpSystem()
    {
        powerUpSpawner.gameObject.SetActive(true);
    }

    private void DisableScoringSystem()
    {
        scoreText.gameObject.SetActive(false);
    }

    private void DisablePowerUpSystem()
    {
        if (DestroyAllPowerUps())
        {
            powerUpSpawner.gameObject.SetActive(false);
        }
    }

    public void EnableSpawner()
    {
        spawner.SetActive(true);
    }

    public void DisableSpawner()
    {
        spawner.SetActive(false);
    }

    public void ReverseGravityMethod(float powerUpDuration, string powerUpMessage)
    {
        EnableGravityReverse();
        powerUpText.text = powerUpMessage;
        StartCoroutine(DisableGravityReverse(powerUpDuration));
    }

    private void EnableGravityReverse()
    {
        player.ReverseGravity();
    }

    IEnumerator DisableGravityReverse(float powerUpDuration)
    {
        yield return new WaitForSecondsRealtime(powerUpDuration);
        player.ReverseGravity();
        powerUpText.text = "";
    }
}
