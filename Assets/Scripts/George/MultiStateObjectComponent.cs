using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiStateObjectComponent : MonoBehaviour
{
    // called when current global time state is changed
    public abstract void OnTimeStateChange(TimeState newTimeState);
    
    // called when player attempts to interact with this object
    public abstract void OnInteract();
}
