using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingToggle : MultiStateObjectComponent
{
    private PostProcessLayer _layer;
    
    private void Awake()
    {
        _layer = GetComponent<PostProcessLayer>();
    }

    public override void OnTimeStateChange(TimeState newTimeState)
    {
        switch (newTimeState)
        {
            case TimeState.Past:
                _layer.enabled = true;
                break;
            
            case TimeState.Present:
                _layer.enabled = false;
                break;
        }
    }

    public override void OnInteract()
    {
        
    }
}
