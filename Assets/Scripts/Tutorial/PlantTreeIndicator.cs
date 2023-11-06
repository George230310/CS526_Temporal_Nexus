using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlantTreeIndicator : MultiStateObjectComponent
{
    private SpriteRenderer _sprite;
    
    private void Start()
    {
        _sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public override void OnTimeStateChange(TimeState newTimeState)
    {
        switch (newTimeState)
        {
            case TimeState.Past:

                if (gameObject.activeSelf)
                {
                    _sprite.DOFade(1.0f, 0.5f);
                }
                
                break;
            
            case TimeState.Present:

                if (gameObject.activeSelf)
                {
                    _sprite.DOFade(0.0f, 0.5f);
                }
                
                break;
        }
    }

    public override void OnInteract()
    {
        
    }
}
