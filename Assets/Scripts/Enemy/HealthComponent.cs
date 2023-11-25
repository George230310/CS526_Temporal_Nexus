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
            PlayerController player = gameObject.GetComponent<PlayerController>();
            Vector3 currentPosition = gameObject.transform.position;
            Checkpoint nearestCheckpoint = null;

            float checkpointDistance = float.MaxValue;
            for (int i = 0; i < player.Checkpoints.Count; ++i)
            {
                Vector3 checkpointPosition = player.Checkpoints[i].PositionAtCheckpoint;
                float distance = Vector3.Distance(currentPosition, checkpointPosition);
                if (distance < checkpointDistance)
                {
                    nearestCheckpoint = player.Checkpoints[i];
                    checkpointDistance = distance;
                }
            }

            if (nearestCheckpoint!= null)
            {
                player.transform.position = nearestCheckpoint.PositionAtCheckpoint;
                health = nearestCheckpoint.HealthAtCheckpoint;
            }

            if (health <= 0f)
            {
                // game over
                GameManager.Instance.EndGame();
            }

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
                GameObject loot = Instantiate(enemy.Loot, transform.position, transform.rotation);
                HealthPickup healthPickup = loot.GetComponent<HealthPickup>();
                if (healthPickup != null)
                {
                    healthPickup.Drop();
                }
            }

            Destroy(gameObject);
        }
    }
}