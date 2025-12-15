using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody2D rb;
    private bool isDead = false;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        rb = GetComponent<Rigidbody2D>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.CompareTag("Coin"))
        {
            if (gameManager != null)
            {
                gameManager.AddScore(1);
            }
            if (AudioManager.Instance != null && AudioManager.Instance.collectCoin != null)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.collectCoin);
            }
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Trap"))
        {
            StartCoroutine(DeathAnimation());
        }
        else if (collision.CompareTag("Down"))
        {
            StartCoroutine(DeathAnimation());
        }
        else if (collision.CompareTag("Enemy"))
        {
            float yDifference = transform.position.y - collision.transform.position.y;
            float xDifference = Mathf.Abs(transform.position.x - collision.transform.position.x);

            if (yDifference > 0 && yDifference > xDifference && rb.linearVelocity.y <= 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 5f);
                Enemy enemy = collision.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Die();
                }
                if (AudioManager.Instance != null && AudioManager.Instance.enemyDeath != null)
                {
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.enemyDeath);
                }
            }
            else
            {
                StartCoroutine(DeathAnimation());
            }
        }
        else if (collision.CompareTag("Finish") || collision.CompareTag("Win"))
        {
            if (gameManager != null)
            {
                gameManager.Win();
            }
        }
    }

    private IEnumerator DeathAnimation()
    {
        isDead = true;

        if (AudioManager.Instance != null && AudioManager.Instance.dead != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.dead);
        }
        PlayerController playerController = GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        Collider2D playerCollider = GetComponent<Collider2D>();
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

        rb.linearVelocity = new Vector2(0f, 10f);

        float spinDuration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < spinDuration)
        {
            elapsedTime += Time.deltaTime;
            float angle = Mathf.Lerp(0f, 360f, elapsedTime / spinDuration);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        if (gameManager != null)
        {
            gameManager.GameOver(0.5f);
        }
        else
        {
            Debug.LogError("GameManager reference is missing.");
        }

        Destroy(gameObject);
    }
}