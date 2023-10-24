using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupMultiStateObject : MultiStateObjectComponent
{
    public GameObject HealthPickup;

    void Awake()
    {
        isInteractable = false;
    }

    public override void OnInteract()
    {
    }

    public override void OnTimeStateChange(TimeState newTimeState)
    {
        switch (newTimeState)
        {
            case TimeState.Present:
                if (HealthPickup != null)
                {
                    HealthPickup?.SetActive(true);
                }
                break;
            case TimeState.Past:
                if (HealthPickup != null)
                {
                    HealthPickup?.SetActive(false);
                }
                break;
        }
    }
}
