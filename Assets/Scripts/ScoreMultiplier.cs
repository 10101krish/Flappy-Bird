using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMultiplier : MonoBehaviour
{
    private GameManager gameManager;
    private PowerUps powerUps;

    private float leftEdge;

    private float speed = 5f;
    private float powerUpDuration = 10f;
    public string powerUpMessage = "Score Multiplier";

    public int multiplierMax = 5;
    public int multiplierMin = 2;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        powerUps = GetComponent<PowerUps>();
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1;
    }

    private void Start()
    {
        this.speed = powerUps.speed;
        this.powerUpDuration = powerUps.powerUpDuration;
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * Vector3.left;

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int multiplier = Random.Range(multiplierMin, multiplierMax);
            gameManager.EnableScoreMultiplier(multiplier, powerUpDuration);
            Destroy(gameObject);
        }
    }
}
