using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class PowerUpSpawner : MonoBehaviour
{
    private GameManager gameManager;
    public PowerUps[] powerUps;

    public float minPowerUpDelay = 20f;
    public float maxPowerUpDelay = 30f;
    public float powerUpTimePeriod = 2f;

    public float nextPowerUpTime;
    public bool powerUpSpawned = false;

    public float minHeight = -1f;
    public float maxHeight = 2f;
    // public float timeSinceLevelLoad = 0;

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
        // timeSinceLevelLoad = Time.timeSinceLevelLoad;
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
        yield return new WaitForSecondsRealtime(powerUpTimePeriod / 2);
        InstantiatePowerUp();
    }

    private void InstantiatePowerUp()
    {
        int index = UnityEngine.Random.Range(0, powerUps.Length);

        Vector3 direction = transform.position;
        direction.y = UnityEngine.Random.Range(minHeight, maxHeight);
        Instantiate(powerUps[index], direction, quaternion.identity);
    }

    IEnumerator PowerUpPeriodLapsedCoroutine()
    {
        yield return new WaitForSecondsRealtime(powerUpTimePeriod);
        gameManager.EnablePipeSpawner();
        powerUpSpawned = false;
    }
}
