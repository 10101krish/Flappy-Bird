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

    private float nextPowerUpTime;
    private bool powerUpSpawned = false;

    public float minHeight = -1f;
    public float maxHeight = 2f;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        powerUpText.text = "";
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
        gameManager.DisableSpawner();
        powerUpSpawned = true;
        nextPowerUpTime = Time.timeSinceLevelLoad + UnityEngine.Random.Range(minPowerUpDelay, maxPowerUpDelay) + powerUpTimePeriod;
        StartCoroutine(SpawnPowerUpCoroutine());
        StartCoroutine(PowerUpPeriodLapsedCoroutine());
    }

    IEnumerator SpawnPowerUpCoroutine()
    {
        yield return new WaitForSecondsRealtime(powerUpTimePeriod / 2);

        Vector3 direction = transform.position;
        direction.y = UnityEngine.Random.Range(minHeight, maxHeight);
        PowerUps spawnedPowerUp = Instantiate(powerUp, direction, quaternion.identity);
        spawnedPowerUp.SetPowerUp("Reverse Gravity", powerUpText, powerUpDuration);
    }

    IEnumerator PowerUpPeriodLapsedCoroutine()
    {
        yield return new WaitForSecondsRealtime(powerUpTimePeriod);
        gameManager.EnableSpawner();
        powerUpSpawned = false;
    }
}
