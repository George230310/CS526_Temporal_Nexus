using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStomp : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("WeakPoint"))
        {
            HealthComponent comp = other.gameObject.GetComponent<HealthComponent>();
            if (comp)
            {
                comp.TakeDamage(100f);
            }
        }
    }
}
