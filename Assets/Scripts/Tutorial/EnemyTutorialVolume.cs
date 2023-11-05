using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyTutorialVolume : MonoBehaviour
{
    [SerializeField] private SpriteRenderer animatedSprite;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animatedSprite.DOFade(0.7f, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animatedSprite.DOFade(0.0f, 0.5f);
        }
    }
}
