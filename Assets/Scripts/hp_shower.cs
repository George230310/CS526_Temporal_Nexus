using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class hp_shower : MonoBehaviour
{
    private HealthComponent _healthComponent;
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI HPText;

    // Update is called once per frame
    void Update()
    {
        if (!_healthComponent)
        {
            _healthComponent = GameManager.Instance.player.GetComponent<HealthComponent>();
            return;
        }

        double currentHealth = Math.Floor(_healthComponent.health);
        HPText.text = "Your HP: " + currentHealth;
        
        healthBar.fillAmount = _healthComponent.health / _healthComponent.maxHealth;
    }
}
