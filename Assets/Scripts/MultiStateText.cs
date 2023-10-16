using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiStateText : MultiStateObjectComponent
{
    private TextMeshPro _multiStateText;
    [TextArea] public String pastText;
    [TextArea] public String currentText;

    private void Start()
    {
        _multiStateText = GetComponent<TextMeshPro>();
    }

    public override void OnTimeStateChange(TimeState newTimeState)
    {
        switch (newTimeState)
        {
            case TimeState.Past:
                _multiStateText.text = pastText;
                break;
            
            case TimeState.Present:
                _multiStateText.text = currentText;
                break;
        }
    }

    public override void OnInteract()
    {
        
    }
}
