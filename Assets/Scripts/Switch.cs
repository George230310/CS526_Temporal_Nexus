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

    private void Start()
    {
        // initialize switch sprite
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _spriteRenderer.color = Color.grey;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // should not do anything if not enabled
        if (!_isSwitchEnabled)
        {
            return;
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < movableBlockEvents.Count; ++i)
            {
                movableBlockEvents[i].Invoke();
            }
        }
    }
    
    // on time state change function of switch
    public override void OnTimeStateChange(TimeState newTimeState)
    {
        // change the state of switch based on current new time state
        switch (newTimeState)
        {
            case TimeState.Past:
                _isSwitchEnabled = true;
                
                // make switch color brighter
                if (_spriteRenderer)
                {
                    _spriteRenderer.color = Color.white;
                }
                
                break;
            
            case TimeState.Present:
                _isSwitchEnabled = false;
                
                // make switch color darker
                if (_spriteRenderer)
                {
                    _spriteRenderer.color = Color.grey;
                }
                
                break;
        }
    }
}
