using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float maxHealth;
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        // if the player died
        if (gameObject.CompareTag("Player"))
        {
            // game over
            GameManager.Instance.EndGame();
        }
        // if the enemy died
        else
        {
            Destroy(gameObject);
        }
    }
}