using System;
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

    public void Heal(float healAmount)
    {
        health = Mathf.Clamp(health + healAmount, 0f, maxHealth);
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
            MultiStateEnemy enemy = GetComponent<MultiStateEnemy>();
            if (enemy == null)
            {
                enemy = GetComponent<MultiStateNormalEnemy>() as MultiStateEnemy;
            }
            if (enemy == null)
            {
                enemy = GetComponent<MultiStateProjectileEnemy>() as MultiStateEnemy;
            }

            if (enemy != null && enemy.Loot != null)
            {
                Instantiate(enemy.Loot, transform.position, transform.rotation);
            }
            
            Destroy(gameObject);
        }
    }
}