using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Text powerUpText;

    public GameObject playButton;
    public GameObject gameOver;
    public GameObject spawner;
    public GameObject powerUpSpawner;

    public Sprite gameOverSprite;
    public Sprite getReadySprite;

    private ScoreSystem scoreSystem;

    public bool ReverseGravityActivated = false;

    private void Awake()
    {
        scoreSystem = GetComponent<ScoreSystem>();
        Application.targetFrameRate = 60;
        gameOver.GetComponent<Image>().sprite = getReadySprite;
        Pause();
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
        EnableAllSystems();
    }


    public void Pause()
    {
        playButton.SetActive(true);
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void GameOver()
    {
        scoreSystem.GameOver();
        DisablePowerUpSystem();
        gameOver.GetComponent<Image>().sprite = gameOverSprite;
        gameOver.SetActive(true);
        playButton.SetActive(true);

        Pause();
    }

    public void IncreaseScore()
    {
        scoreSystem.IncreaseScore(1);
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

    private void EnableAllSystems()
    {
        EnablePowerUpSystem();
        EnableScoringSystem();
        EnablePipeSpawner();
    }

    private void EnableScoringSystem()
    {
        scoreSystem.EnableScoringSystem();
    }

    private void EnablePowerUpSystem()
    {
        powerUpSpawner.gameObject.SetActive(true);
    }

    private void DisableScoringSystem()
    {
        scoreSystem.DisableScoringSystem();
    }

    private void DisablePowerUpSystem()
    {
        if (DestroyAllPowerUps())
        {
            powerUpSpawner.gameObject.SetActive(false);
        }
    }

    public void EnablePipeSpawner()
    {
        spawner.SetActive(true);
    }

    public void DisablePipeSpawner()
    {
        spawner.SetActive(false);
    }

    public void ReverseGravityMethod(string powerUpMessage)
    {
        player.ReverseGravity();
        if (!ReverseGravityActivated)
        {
            ReverseGravityActivated = true;
            powerUpText.text = powerUpMessage;
        }
        else
        {
            ReverseGravityActivated = false;
            powerUpText.text = "";
        }
    }
}
