using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerUps : MonoBehaviour
{
    private GameManager gameManager;

    public Text powerUpText;
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

    public void SetPowerUp(string powerUpMessage, Text powerUpText, float powerUpDuration)
    {
        this.powerUpDuration = powerUpDuration;
        this.powerUpText = powerUpText;
        this.powerUpMessage = powerUpMessage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            EnforcePower();
            StartCoroutine(RemovePower());
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void EnforcePower()
    {
        gameManager.ReverseGravity();
        powerUpText.text = powerUpMessage;
    }

    IEnumerator RemovePower()
    {
        yield return new WaitForSecondsRealtime(powerUpDuration);
        gameManager.ReverseGravity();
        powerUpText.text = "";
        Destroy(this.gameObject);
    }
}
