using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardPuzzleTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.isHardPuzzlePassed = true;
        }
    }
}
