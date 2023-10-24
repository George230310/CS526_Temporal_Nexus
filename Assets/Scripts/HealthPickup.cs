using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float HealAmount = 40f;

    public TextMeshPro HealAmountText;

    void Awake()
    {
        HealAmountText.text = $"+{HealAmount} HP";
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthComponent comp = other.gameObject.GetComponent<HealthComponent>();
            comp.Heal(HealAmount);
            Destroy(gameObject);
        }
    }
}
