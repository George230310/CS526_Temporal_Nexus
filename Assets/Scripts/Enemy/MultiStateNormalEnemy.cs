using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiStateNormalEnemy : MultiStateEnemy
{
    [SerializeField] bool monstershowpast;
    public override void OnTimeStateChange(TimeState newTimeState)
    {
        switch (newTimeState)
        {
            case TimeState.Past:
                if (monstershowpast)
                {
                    gameObject.SetActive(true);
                }
                else { gameObject.SetActive(false); }
                break;
            
            case TimeState.Present:
                if (monstershowpast)
                {
                    gameObject.SetActive(false);
                }
                else { gameObject.SetActive(true); }
               
                break;
        }
    }

    public override void OnInteract()
    {
        
    }
}
