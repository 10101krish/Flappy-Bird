using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Text reverseGravityText;
    public Text scoreMultiplierText;

    public GameObject playButton;
    public GameObject gameOver;
    public GameObject spawner;
    public GameObject powerUpSpawner;

    public Sprite gameOverSprite;
    public Sprite getReadySprite;

    private ScoreSystem scoreSystem;

    private bool reverseGravityActivated = false;

    private int scoreMultiplier = 1;
    private float multiplierDuration;
    private float mutiplierStartTime;
    public float timeSinceLevelLoad;

    private void Awake()
    {
        scoreSystem = GetComponent<ScoreSystem>();
        Application.targetFrameRate = 60;
        gameOver.GetComponent<Image>().sprite = getReadySprite;
        Pause();
    }

    private void Update()
    {
        if (scoreMultiplier != 1 && ((multiplierDuration + mutiplierStartTime) < Time.timeSinceLevelLoad))
            DisableScoreMultiplier();
        timeSinceLevelLoad = Time.timeSinceLevelLoad;
    }

    public void Play()
    {
        reverseGravityText.text = "";
        DisableScoreMultiplier();
        DisableScoringSystem();
        StartCoroutine(DisplayGetReady());

        playButton.SetActive(false);
        player.enabled = true;
        DestroyAllPipes();
    }

    IEnumerator DisplayGetReady()
    {
        gameOver.GetComponent<Image>().sprite = getReadySprite;
        EnableScoringSystem();
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
        scoreSystem.IncreaseScore(1 * scoreMultiplier);
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
        if (!reverseGravityActivated)
        {
            reverseGravityActivated = true;
            reverseGravityText.text = "Reverse Gravity";
        }
        else
        {
            reverseGravityActivated = false;
            reverseGravityText.text = "";
        }
    }

    public void EnableScoreMultiplier(int multiplier, float powerUpDuration)
    {
        scoreMultiplier *= multiplier;
        multiplierDuration = powerUpDuration;
        mutiplierStartTime = Time.timeSinceLevelLoad;
        scoreMultiplierText.text = scoreMultiplier.ToString() + "X";
    }

    private void DisableScoreMultiplier()
    {
        scoreMultiplierText.text = "";
        scoreMultiplier = 1;
    }
}
