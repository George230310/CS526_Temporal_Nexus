using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private GameManager gameManager;
    private bool canTakeDamage = true;
    private float damageCooldown = 0.5f; // Adjust the cooldown duration as needed.
    private float damageRate = 2f;     // Adjust the rate of damage as needed.
    private HealthComponent playerHealth;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (canTakeDamage && other.gameObject.CompareTag("Player"))
        {
            playerHealth = other.gameObject.GetComponent<HealthComponent>();

            HealthComponent comp = playerHealth;
            comp.TakeDamage(10f);

            canTakeDamage = false; // Disable damage temporarily.
            StartCoroutine(EnableDamageAfterCooldown());

            //StartCoroutine(ShakePlayer(other.transform, 0.1f, 0.1f, 0.1f));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealth = null; // Player is no longer in the spike area.
        }
    }

    IEnumerator EnableDamageAfterCooldown()
    {
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true; // Enable damage after the cooldown.
    }

    IEnumerator DamageOverTime()
    {
        while (playerHealth != null)
        {
            playerHealth.TakeDamage(5f); // Adjust the damage per 2 seconds as needed.
            yield return new WaitForSeconds(2f); // Damage rate is every 2 seconds.
        }
    }

    [CanBeNull]
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

        playerTransform.position = originalPosition;
    }
}
