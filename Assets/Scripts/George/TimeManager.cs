using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TimeState
{
    Present,
    Past
}

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    private List<MultiStateObjectComponent> _multiStateObjectComponents;
    public TimeState CurrentGlobalTimeState { get; private set; }

    private void Awake()
    {
        // singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    // Start is called before the first frame update
    private void Start()
    {
        // initialize current global time state to be present
        CurrentGlobalTimeState = TimeState.Present;
        
        // get all multi-state object components
        _multiStateObjectComponents = FindObjectsByType<MultiStateObjectComponent>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        
        // call state change to present on all multi-state objects
        foreach (var multiState in _multiStateObjectComponents)
        {
            multiState.OnTimeStateChange(CurrentGlobalTimeState);
        }
    }

    public void ChangeCurrentGlobalTimeState(TimeState newTimeState)
    {
        // if the new time state is different, change global time state and call on time state change on each multi-state objects
        if (newTimeState != CurrentGlobalTimeState)
        {
            CurrentGlobalTimeState = newTimeState;
            foreach (var multiState in _multiStateObjectComponents)
            {
                multiState.OnTimeStateChange(CurrentGlobalTimeState);
            }
        }
    }
}
