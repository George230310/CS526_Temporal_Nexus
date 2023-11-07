using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiStateNormalEnemy : MultiStateEnemy
{
    [SerializeField] private bool hideInPast = true;
    
    public override void OnTimeStateChange(TimeState newTimeState)
    {
        switch (newTimeState)
        {
            case TimeState.Past:
                gameObject.SetActive(!hideInPast);
                break;
            
            case TimeState.Present:
                gameObject.SetActive(true);
                break;
        }
    }

    public override void OnInteract()
    {
        
    }
}
