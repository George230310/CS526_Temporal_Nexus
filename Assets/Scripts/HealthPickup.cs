using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class HealthPickup : MonoBehaviour
{
    public float PickupDelay = 1f;

    public float HealAmount = 40f;

    public TextMeshPro HealAmountText;
    
    [SerializeField] private HealthPickupMultiStateObject myMultiStateObject;

    private bool isDropped = true;

    void Awake()
    {
        HealAmountText.text = $"+{HealAmount} HP";
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isDropped)
        {
            HealthComponent comp = other.gameObject.GetComponent<HealthComponent>();
            comp.Heal(HealAmount);

            if (myMultiStateObject)
            {
                myMultiStateObject.HealthPickup = null;
            }
            
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isDropped)
        {
            HealthComponent comp = other.gameObject.GetComponent<HealthComponent>();
            comp.Heal(HealAmount);

            if (myMultiStateObject)
            {
                myMultiStateObject.HealthPickup = null;
            }
            
            Destroy(gameObject);
        }
    }

    public void Drop()
    {
        isDropped = false;

        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Color activeColor = spriteRenderer.color;
        Color inactiveColor = spriteRenderer.color;
        inactiveColor.a = 0;
        spriteRenderer.color = inactiveColor;

        spriteRenderer.DOColor(activeColor, PickupDelay).OnComplete(() => { isDropped = true; });
    }
}
