using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MultiStateObjectComponent
{
    public override void OnTimeStateChange(TimeState newTimeState)
    {
        switch (newTimeState)
        {
            case TimeState.Past:
                gameObject.SetActive(true);
                break;
            
            case TimeState.Present:
                gameObject.SetActive(false);
                break;
        }
    }

    public override void OnInteract()
    {
        
    }
}
