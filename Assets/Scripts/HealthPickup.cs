using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float HealAmount = 20f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthComponent comp = other.gameObject.GetComponent<HealthComponent>();
            comp.Heal(HealAmount);
            Destroy(this);
        }
    }
}
