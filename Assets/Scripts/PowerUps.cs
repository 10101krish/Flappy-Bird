using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerUps : MonoBehaviour
{
    private GameManager gameManager;


    private string powerUpMessage;

    public float speed = 5f;
    private float leftEdge;
    private float powerUpDuration;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1;
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * Vector3.left;

        if (transform.position.x < leftEdge)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetPowerUp(string powerUpMessage, float powerUpDuration)
    {
        this.powerUpDuration = powerUpDuration;
        this.powerUpMessage = powerUpMessage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.ReverseGravityMethod(powerUpMessage);
            Destroy(gameObject);
        }
    }
}
