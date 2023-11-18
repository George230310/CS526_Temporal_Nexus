using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isCheckpointSet;

    public Vector3 PositionAtCheckpoint;

    public float HealthAtCheckpoint;

    void Awake()
    {
        isCheckpointSet = false;
        HealthAtCheckpoint = 0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCheckpointSet)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            HealthComponent health = other.GetComponent<HealthComponent>();

            PositionAtCheckpoint = player.gameObject.transform.position;
            HealthAtCheckpoint = health.health;
            player.Checkpoint = this;

            isCheckpointSet = true;

            gameObject.SetActive(false);
        }
    }
}
