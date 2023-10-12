using UnityEngine;

public class ReverseGravity : MonoBehaviour
{
    private GameManager gameManager;
    private PowerUps powerUps;

    private float leftEdge;

    private float speed = 5f;
    public string powerUpMessage = "Reverse Gravity";

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        powerUps = GetComponent<PowerUps>();
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1;
    }

    private void Start()
    {
        this.speed = powerUps.speed;
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
            gameManager.ReverseGravityMethod(powerUpMessage);
            Destroy(gameObject);
        }
    }
}
