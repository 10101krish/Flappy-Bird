using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpSpawner : MonoBehaviour
{
    private GameManager gameManager;
    public PowerUps powerUp;

    public float powerUpDuration = 2f;
    public float minPowerUpDelay = 10f;
    public float maxPowerUpDelay = 10f;
    public float powerUpTimePeriod = 2f;

    public float nextPowerUpTime;
    public bool powerUpSpawned = false;

    public float minHeight = -1f;
    public float maxHeight = 2f;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        nextPowerUpTime = Time.timeSinceLevelLoad + UnityEngine.Random.Range(minPowerUpDelay, maxPowerUpDelay);
    }

    private void Update()
    {
        if (!powerUpSpawned && Time.timeSinceLevelLoad >= nextPowerUpTime)
        {
            SpawnPowerUp();
        }
    }

    private void SpawnPowerUp()
    {
        gameManager.DisablePipeSpawner();
        powerUpSpawned = true;
        nextPowerUpTime = Time.timeSinceLevelLoad + UnityEngine.Random.Range(minPowerUpDelay, maxPowerUpDelay);
        StartCoroutine(SpawnPowerUpCoroutine());
        StartCoroutine(PowerUpPeriodLapsedCoroutine());
    }

    IEnumerator SpawnPowerUpCoroutine()
    {
        yield return new WaitForSecondsRealtime(powerUpTimePeriod / 2f);
        InstantiatePowerUp();
    }

    IEnumerator PowerUpPeriodLapsedCoroutine()
    {
        yield return new WaitForSecondsRealtime(powerUpTimePeriod);
        gameManager.EnablePipeSpawner();
        powerUpSpawned = false;
    }

    private void InstantiatePowerUp()
    {
        Vector3 direction = transform.position;
        direction.y = UnityEngine.Random.Range(minHeight, maxHeight);
        PowerUps spawnedPowerUp = Instantiate(powerUp, direction, quaternion.identity);
        spawnedPowerUp.SetPowerUp("Reverse Gravity", nextPowerUpTime - Time.timeSinceLevelLoad - 1);
    }
}
