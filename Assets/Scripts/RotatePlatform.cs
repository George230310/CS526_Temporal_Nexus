using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotatePlatform : MonoBehaviour
{
    [SerializeField] private float rotateAngle = -90f;

    private bool _isRotated;
    
    [ContextMenu("Rotate")]
    public void Rotate()
    {
        if (!_isRotated)
        {
            Quaternion axisRotation = Quaternion.AngleAxis(rotateAngle, Vector3.forward);
            Quaternion targetRotation = axisRotation * transform.rotation;
            transform.DORotate(targetRotation.eulerAngles, 0.5f);
            _isRotated = true;
        }
    }
}
