using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPrompt : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().tutorialJumpEnabled = true;
        }
    }
}
