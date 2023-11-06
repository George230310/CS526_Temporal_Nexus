using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MovableBlock : MonoBehaviour
{
    private bool _isMovedAway;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    
    // is this block supposed to move vertically?
    [SerializeField] private bool isVerticalMove;

    // The starting position must be calculated dynamically if the parent block moves.
    [SerializeField] private bool isPartOfMovingPlatform = false;
    
    // how much to move
    [SerializeField] private float moveOffset;
    [SerializeField] private float moveDuration;

    void Awake()
    {
        _startPosition = transform.position;
    }

    // function to be invoked when switch is toggled
    public void MoveBlock()
    {
        if (isPartOfMovingPlatform)
        {
            _startPosition = transform.position;
        }

        if (isVerticalMove)
        {
            _endPosition = _startPosition + Vector3.up * moveOffset;
        }
        else
        {
            _endPosition = _startPosition + Vector3.right * moveOffset;
        }
        
        if (_isMovedAway)
        {
            MoveBack();
            _isMovedAway = false;
        }
        else
        {
            MoveAway();
            _isMovedAway = true;
        }
    }
    
    private void MoveAway()
    {
        transform.DOMove(_endPosition, moveDuration);
    }

    private void MoveBack()
    {
        transform.DOMove(_startPosition, moveDuration);
    }
}
