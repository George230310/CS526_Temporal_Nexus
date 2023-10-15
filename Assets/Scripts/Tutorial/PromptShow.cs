using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptShow : MonoBehaviour
{
    public GameObject Prompt;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Prompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Prompt.SetActive(false);
        }
    }
}
