using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TPressDetector : MonoBehaviour
{
    [SerializeField] private GameObject tPrompt;
    [SerializeField] private SpriteRenderer plantTreeIndicator;
    [SerializeField] private Tree treeToPlant;

    private int _tPressCount;
    private bool _isDetecting = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PressT();
        }
    }

    private void PressT()
    {
        if (!_isDetecting)
        {
            return;
        }
        
        _tPressCount++;

        if (_tPressCount == 1)
        {
            _isDetecting = false;
            tPrompt.SetActive(false);
            plantTreeIndicator.DOFade(1.0f, 0.5f);
        }
        else if (_tPressCount == 2)
        {
            _isDetecting = false;
            tPrompt.SetActive(false);
        }
    }

    private void EnableTPrompt()
    {
        _isDetecting = true;
        tPrompt.SetActive(true);
    }
}
