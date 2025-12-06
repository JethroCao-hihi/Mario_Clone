using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            gameManager.AddScore(1);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Trap"))
        {
            Debug.Log("Dau");
        }
        else if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Game Over");
        }
        else if (collision.CompareTag("Finish"))
        {
            Debug.Log("Win");
        }
        else if (collision.CompareTag("Win"))
        {
            Debug.Log("You Win!");
        }
    }
}
