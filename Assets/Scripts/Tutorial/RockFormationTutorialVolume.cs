using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RockFormationTutorialVolume : MonoBehaviour
{
    [SerializeField] private SpriteRenderer animatedSprite;

    private bool _isTeaching;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && animatedSprite.gameObject.activeSelf)
        {
            _isTeaching = true;
            animatedSprite.DOFade(1.0f, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && animatedSprite.gameObject.activeSelf)
        {
            _isTeaching = false;
            animatedSprite.DOFade(0.0f, 0.5f);
        }
    }

    private void Update()
    {
        if (_isTeaching)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                animatedSprite.gameObject.SetActive(false);
            }
        }
    }
}
