using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovablePlatform : MonoBehaviour
{
    [SerializeField] private float speed = 10f; // Speed at which the platform moves.
    [SerializeField] private float moveDistance = 20f; // Distance the platform moves before changing direction.

    private bool _shouldPlatformMove;
    
    private Vector3 startPosition;
    private bool movingRight = true; // Determines the direction the platform is moving.

    private Tween moveTween = null;

    public void EnablePlatformMove()
    {
        _shouldPlatformMove = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position; // Save the initial position.
    }

    // Update is called once per frame
    void Update()
    {
        if (!_shouldPlatformMove)
        {
            return;
        }

        if (moveTween == null || moveTween.IsComplete())
        {
            moveTween = transform.DOLocalMoveX(transform.localPosition.x + moveDistance, moveDistance / speed)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }
        
    //     // calculate the platform's current position based on its direction.
    //     if (movingright)
    //     {
    //         transform.translate(vector3.right * speed * time.deltatime, space.world);
    //         if (transform.position.x > startposition.x + movedistance)
    //         {
    //             movingright = false; // change direction if it moved far enough.
    //         }
    //     }
    //     else
    //     {
    //         transform.translate(vector3.left * speed * time.deltatime, space.world);
    //         if (transform.position.x < startposition.x)
    //         {
    //             movingright = true; // change direction if it moved back to the start.
    //         }
    //     }
    }
}
