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
                    _spriteRenderer.color = Color.yellow;
                }
                
                break;
        }
    }

    public override void OnInteract()
    {
        if (_isSwitchEnabled)
        {
            GameManager.Instance.gameHUD.optionDescription.text = "You moved the switch";
            GameManager.Instance.gameHUD.PresentInteractionMessageOnly();
            
            foreach (var e in movableBlockEvents)
            {
                e.Invoke();
            }
        }
        else
        {
            GameManager.Instance.gameHUD.optionDescription.text = "The switch is too rusty to move now. maybe it worked a long time ago...";
            GameManager.Instance.gameHUD.PresentInteractionMessageOnly();
        }
    }
}
