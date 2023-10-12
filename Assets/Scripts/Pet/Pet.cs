using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MultiStateObjectComponent
{
    private bool _isSaved;
    private bool _canBeSaved;

    public void SavePet()
    {
        if (_canBeSaved)
        {
            _isSaved = true;
        }
    }

    public override void OnTimeStateChange(TimeState newTimeState)
    {
        switch (newTimeState)
        {
            case TimeState.Past:
                _canBeSaved = true;
                gameObject.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
                break;
            
            case TimeState.Present:
                _canBeSaved = false;
                
                // scale the pet based on whether it is saved in the past
                gameObject.transform.localScale = _isSaved ? new Vector3(10.0f, 10.0f, 10.0f) : new Vector3(1.0f, 1.0f, 1.0f);
                
                break;
        }
    }

    public override void OnInteract()
    {
        SavePet();
    }
}
