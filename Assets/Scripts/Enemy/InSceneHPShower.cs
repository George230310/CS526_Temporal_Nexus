using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InSceneHPShower : MonoBehaviour
{
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private Image healthBar;

    // Update is called once per frame
    void Update()
    {
        if (healthBar && healthComponent)
        {
            healthBar.fillAmount = healthComponent.health / healthComponent.maxHealth;
        }
    }
}
