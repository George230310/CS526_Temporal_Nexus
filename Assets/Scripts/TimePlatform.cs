using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TimePlatform : MultiStateObjectComponent
{
    private Vector3 _pastPosition;
    private Vector3 _presentPosition;

    private Vector3 _targetPosition;
    
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
                _targetPosition = _pastPosition;
                
                break;
            
            case TimeState.Past:
                _targetPosition = _presentPosition;
                
                break;
        }
    }

    private void Update()
    {
        float step = 20f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);
    }

    public override void OnInteract()
    {
        
    }
}
