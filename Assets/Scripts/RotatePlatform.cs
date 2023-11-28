using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotatePlatform : MonoBehaviour
{
    [SerializeField] private float rotateAngle = -90f;

    private bool _isRotated;

    private Quaternion _endRot;
    private Quaternion _startRot;

    private Quaternion _targetRot;

    private void Start()
    {
        _startRot = transform.rotation;
        _endRot = transform.rotation * Quaternion.AngleAxis(rotateAngle, Vector3.forward);
    }

    [ContextMenu("Rotate")]
    public void Rotate()
    {
        if (!_isRotated)
        {
            _targetRot = _endRot;
            _isRotated = true;
        }
        else
        {
            _targetRot = _startRot;
            _isRotated = false;
        }
    }

    private void Update()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, _targetRot, 0.01f);
    }
}
