using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _shootDir;
    [SerializeField] private float speed = 30f;
    [SerializeField] private float lifeSpan = 3f;

    private void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    public void Setup(Vector3 shootDir)
    {
        _shootDir = shootDir;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += _shootDir * (speed * Time.deltaTime);
    }
}
