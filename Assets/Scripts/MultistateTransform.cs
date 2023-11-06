using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultistateTransform : MultiStateObjectComponent
{
    public GameObject PastObject;

    public GameObject PresentObject;

    void Awake()
    {
        isInteractable = false;
    }

    public override void OnInteract()
    {
    }

    public override void OnTimeStateChange(TimeState newTimeState)
    {
        PastObject.SetActive(false);
        PresentObject.SetActive(false);

        switch (newTimeState)
        {
            case TimeState.Present:
                PresentObject.SetActive(true);
                break;
            case TimeState.Past:
                PastObject.SetActive(true);
                break;
        }
    }
}
