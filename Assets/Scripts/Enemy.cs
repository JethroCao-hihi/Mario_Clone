using System;
using System.Collections;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float distance = 5f;
    private Vector3 startPos;
    private bool movingRight = false;
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        startPos = transform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;
        float leftBound = startPos.x - distance;
        float rightBound = startPos.x + distance;
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            if (transform.position.x >= rightBound)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            if (transform.position.x <= leftBound)
            {
                movingRight = true;
                Flip();
            }
        }
        UpdateAnimation();
    }
    void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    private void UpdateAnimation()
    {
        bool isMoving = Mathf.Abs(moveSpeed) > 0.1f;
        animator.SetBool("isMoving", isMoving);
    }
    public void Die()
    {
        if (!isDead)
        {
            StartCoroutine(SinkAndDestroy());
        }
    }
    private IEnumerator SinkAndDestroy() {
        isDead = true;
        Collider2D enemmyCollider = GetComponent<Collider2D>();
        if (enemmyCollider != null)
        {
            enemmyCollider.enabled = false;
        }

        if (animator != null)
        {
            animator.enabled = false;
        }

        float shrinkDuration = 3f;
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = new Vector3(startScale.x, 0f, startScale.z); // Giảm chiều cao xuống 0
        float elapsedTime = 0f;

        // Lưu vị trí ban đầu để giữ đáy enemy cố định
        float initialY = transform.position.y;
        float initialHeight = Mathf.Abs(startScale.y);

        while (elapsedTime < shrinkDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / shrinkDuration;

            // Co lại chiều cao
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);

            // Di chuyển xuống để giữ đáy enemy cố định
            float currentHeight = Mathf.Abs(transform.localScale.y);
            float heightDifference = initialHeight - currentHeight;
            transform.position = new Vector3(transform.position.x, initialY - (heightDifference / 2f), transform.position.z);

            yield return null;
        }
        Destroy(gameObject);
    }
}
