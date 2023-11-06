using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPressDetector : MonoBehaviour
{
    [SerializeField] private GameObject tPrompt;

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
            Invoke(nameof(EnableTPrompt), 2f);
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
