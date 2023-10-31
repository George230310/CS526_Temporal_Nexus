using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TimePlatform : MultiStateObjectComponent
{
    private Vector3 _pastPosition;
    private Vector3 _presentPosition;
    private Tween _myTween;
    
    [SerializeField] private float moveDuration;
    [SerializeField] private float moveOffset;
    private void Awake()
    {
        _pastPosition = transform.position + Vector3.up * moveOffset;
        _presentPosition = transform.position;
    }

    public override void OnTimeStateChange(TimeState newTimeState)
    {
        switch (newTimeState)
        {
            case TimeState.Present:
                transform.DOMove(_presentPosition, moveDuration);
                
                break;
            
            case TimeState.Past:
                transform.DOMove(_pastPosition, moveDuration);
                
                break;
        }
    }

    public override void OnInteract()
    {
        
    }
}
