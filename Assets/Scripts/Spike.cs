using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private GameManager gameManager;
    
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HealthComponent comp = other.gameObject.GetComponent<HealthComponent>();
            comp.TakeDamage(20f);

            // Call the ShakePlayer method to make the player vibrate.
            StartCoroutine(ShakePlayer(other.transform, 0.1f, 0.1f, 0.1f));
        }
    }

    // Coroutine to shake the player.
    IEnumerator ShakePlayer(Transform playerTransform, float duration, float magnitudeX, float magnitudeY)
    {
        Vector3 originalPosition = playerTransform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float xOffset = Random.Range(-3f, 3f) * magnitudeX;
            float yOffset = Random.Range(-3f, 3f) * magnitudeY;

            playerTransform.position = originalPosition + new Vector3(xOffset, yOffset, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset the player's position.
        playerTransform.position = originalPosition;
    }
}
