using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HealthComponent comp = other.gameObject.GetComponent<HealthComponent>();
            if (comp)
            {
                comp.TakeDamage(damage);
            }
        }
    }
}