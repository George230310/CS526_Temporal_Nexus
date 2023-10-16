using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    public int damage;
    [SerializeField] private float collisionDelta = 1.8f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // must check collision direction
            Vector3 otherPosition = other.gameObject.transform.position;
            Vector3 myPosition = gameObject.transform.position;

            bool hitFromTop = (otherPosition.y - myPosition.y > collisionDelta);
            
            // if enemy gets hit from top, enemy is stomped
            if (hitFromTop)
            {
                HealthComponent myHealth = GetComponent<HealthComponent>();
                if (myHealth)
                {
                    myHealth.TakeDamage(100.0f);
                }
            }
            // else deal damage to player
            else
            {
                HealthComponent comp = other.gameObject.GetComponent<HealthComponent>();
                if (comp)
                {
                    comp.TakeDamage(damage);
                }
            }
            
        }
    }
}
