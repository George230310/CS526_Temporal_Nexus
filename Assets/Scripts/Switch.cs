using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Switch : MultiStateObjectComponent
{
    public List<UnityEvent> movableBlockEvents;

    private SpriteRenderer _spriteRenderer;
    
    // bool determining if this switch is enabled
    private bool _isSwitchEnabled;

    public bool isDataSender;

    private void Start()
    {
        // initialize switch sprite
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _spriteRenderer.color = Color.yellow;
    }
    
    // on time state change function of switch
    public override void OnTimeStateChange(TimeState newTimeState)
    {
        // change the state of switch based on current new time state
        switch (newTimeState)
        {
            case TimeState.Past:
                _isSwitchEnabled = true;
                isInteractable = true;
                
                // make switch color brighter
                if (_spriteRenderer)
                {
                    _spriteRenderer.color = Color.white;
                }
                
                break;
            
            case TimeState.Present:
                _isSwitchEnabled = false;
                isInteractable = false;
                
                // make switch color darker
                if (_spriteRenderer)
                {
                    _spriteRenderer.color = Color.yellow;
                }
                
                break;
        }
    }

    public override void OnInteract()
    {
        if (_isSwitchEnabled)
        {
            if (isDataSender)
            {
                GameManager.Instance.isMiddleDoorOpened = true;
            }
            
            foreach (var e in movableBlockEvents)
            {
                e.Invoke();
            }
        }
    }
}
