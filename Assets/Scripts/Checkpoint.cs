using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isCheckpointSet;

    public Vector3 PositionAtCheckpoint;

    public float HealthAtCheckpoint;

    public float MinHealthAtCheckpoint;

    void Awake()
    {
        isCheckpointSet = false;
        HealthAtCheckpoint = 0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: Store enemy state so that they respawn too.
        if (other.CompareTag("Player") && !isCheckpointSet)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            HealthComponent health = other.GetComponent<HealthComponent>();

            PositionAtCheckpoint = player.gameObject.transform.position;
            HealthAtCheckpoint = Mathf.Max(health.health, MinHealthAtCheckpoint);
            player.Checkpoints.Add(this);

            isCheckpointSet = true;
        }
    }
}
