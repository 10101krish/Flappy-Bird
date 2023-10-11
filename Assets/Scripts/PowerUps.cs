using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    private GameManager gameManager;

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
            Destroy(gameObject);
    }

    public void SetPowerUpDuration(float powerUpDuration)
    {
        this.powerUpDuration = powerUpDuration;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnforcePower();
        StartCoroutine(RemovePower());
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void EnforcePower()
    {
        gameManager.ReverseGravity();
    }

    IEnumerator RemovePower()
    {
        yield return new WaitForSecondsRealtime(powerUpDuration);
        gameManager.ReverseGravity();
    }
}
