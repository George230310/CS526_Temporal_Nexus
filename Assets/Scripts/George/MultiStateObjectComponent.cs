using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiStateObjectComponent : MonoBehaviour
{
    public abstract void OnTimeStateChange(TimeState newTimeState);
}
