using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class Checkpoint : MonoBehaviour
{
    private bool isCheckpointSet;

    public Vector3 PositionAtCheckpoint;

    public float HealthAtCheckpoint;

    public float MinHealthAtCheckpoint;

    public TextMeshPro CheckpointTakenText;

    public TextMeshPro CheckpointMarkerText;

    void Awake()
    {
        isCheckpointSet = false;
        HealthAtCheckpoint = 0f;
        CheckpointMarkerText.gameObject.SetActive(true);
        CheckpointTakenText.gameObject.SetActive(false);
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

            CheckpointMarkerText.gameObject.SetActive(false);
            CheckpointTakenText.gameObject.SetActive(true);
            CheckpointTakenText.gameObject.transform.DOJump(CheckpointTakenText.gameObject.transform.position + (Vector3.up * 3), 1, 1, 1f)
                .OnComplete(() => { CheckpointTakenText.gameObject.SetActive(false); CheckpointMarkerText.gameObject.SetActive(true);});

            CheckpointMarkerText.text = "Checkpoint saved";
            CheckpointMarkerText.color = Color.green;

            isCheckpointSet = true;
        }
    }
}
