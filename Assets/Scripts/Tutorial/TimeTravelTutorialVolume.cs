using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class TimeTravelTutorialVolume : MonoBehaviour
{
    [SerializeField] private SpriteRenderer animatedSprite;
    [SerializeField] private TPressDetector detector;

    private void Start()
    {
        detector.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && animatedSprite.gameObject.activeSelf)
        {
            detector.enabled = true;
            animatedSprite.DOFade(0.7f, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && animatedSprite.gameObject.activeSelf)
        {
            detector.enabled = false;
            animatedSprite.DOFade(0.0f, 0.5f);
        }
    }
}
