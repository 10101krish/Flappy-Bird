using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    private GameManager gameManager;
    public PowerUps powerUp;

    public float minPowerUpDelay = 2f;
    public float maxPowerUpDelay = 5f;
    public float powerUpTimePeriod = 2f;
    public float powerUpDuration = 2f;

    public float nextPowerUpTime;
    public float powerUpSpawnTime;
    public float powerUpLapseTime;
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
        timePeriodSpawned = false;
        nextPowerUpTime = Time.timeSinceLevelLoad + UnityEngine.Random.Range(minPowerUpDelay, maxPowerUpDelay);
        powerUpSpawnTime = 0;
        powerUpLapseTime = 0;
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
        powerUpSpawnTime = nextPowerUpTime + powerUpTimePeriod / 2;
        powerUpLapseTime = nextPowerUpTime + powerUpTimePeriod;
        nextPowerUpTime = Time.timeSinceLevelLoad + UnityEngine.Random.Range(minPowerUpDelay, maxPowerUpDelay) + powerUpTimePeriod;
        StartCoroutine(SpawnPowerUp());
        StartCoroutine(PowerUpPeriodLapsed());
    }

    IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSecondsRealtime(powerUpTimePeriod / 2);
        Debug.Log("PowerUp Spawned");

        Vector3 direction = transform.position;
        direction.y = UnityEngine.Random.Range(minHeight, maxHeight);
        PowerUps spawnedPowerUp = Instantiate(powerUp, direction, quaternion.identity);
        spawnedPowerUp.SetPowerUpDuration(powerUpDuration);
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
