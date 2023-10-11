using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpSpawner : MonoBehaviour
{
    private GameManager gameManager;
    public PowerUps powerUp;

    public Text powerUpText;
    public float powerUpDuration = 10f;

    public float minPowerUpDelay = 5f;
    public float maxPowerUpDelay = 10f;
    public float powerUpTimePeriod = 2f;

    public float nextPowerUpTime;
    public bool timePeriodSpawned = false;
    public bool powerUpSpawned = false;

    public float minHeight = -1f;
    public float maxHeight = 2f;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        powerUpText.text = "";
        timePeriodSpawned = false;
        nextPowerUpTime = Time.timeSinceLevelLoad + UnityEngine.Random.Range(minPowerUpDelay, maxPowerUpDelay);
    }

    private void Update()
    {
        if (!timePeriodSpawned && Time.timeSinceLevelLoad >= nextPowerUpTime)
        {
            SpawnPowerUpMethod();
        }
    }

    private void SpawnPowerUpMethod()
    {
        gameManager.DisableSpawner();
        timePeriodSpawned = true;
        nextPowerUpTime = Time.timeSinceLevelLoad + UnityEngine.Random.Range(minPowerUpDelay, maxPowerUpDelay) + powerUpTimePeriod/2 + powerUpDuration;
        StartCoroutine(SpawnPowerUp());
        StartCoroutine(PowerUpPeriodLapsed());
    }

    IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSecondsRealtime(powerUpTimePeriod / 2);

        Vector3 direction = transform.position;
        direction.y = UnityEngine.Random.Range(minHeight, maxHeight);
        PowerUps spawnedPowerUp = Instantiate(powerUp, direction, quaternion.identity);
        spawnedPowerUp.SetPowerUp("Reverse Gravity", powerUpText, powerUpDuration);
        powerUpSpawned = true;
    }

    IEnumerator PowerUpPeriodLapsed()
    {
        yield return new WaitForSecondsRealtime(powerUpTimePeriod);
        gameManager.EnableSpawner();
        timePeriodSpawned = false;
        powerUpSpawned = false;
    }
}
