using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager gameManager;

    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;

    private Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 5f;

    private float reverseGravity = 1;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        transform.position = Vector3.zero;
        direction = Vector3.zero;
        reverseGravity = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength * reverseGravity;
        }

        direction.y += gravity * reverseGravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    private void AnimateSprite()
    {
        spriteIndex = (spriteIndex + 1) % sprites.Length;
        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Scoring"))
        {
            gameManager.IncreaseScore();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            gameManager.GameOver();
        }
    }

    public void ReverseGravity()
    {
        // transform.position = Vector3.zero;
        direction = Vector3.zero;
        reverseGravity *= -1f;
    }
}
