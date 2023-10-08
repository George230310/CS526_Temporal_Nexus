using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    public List<UnityEvent> MovableBlockEvents;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < MovableBlockEvents.Count; ++i)
            {
                MovableBlockEvents[i].Invoke();
            }
        }
    }
}
