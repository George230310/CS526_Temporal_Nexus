using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MultiStateObjectComponent
{
    private Vector3 _shootDir;
    [SerializeField] private float speed = 30f;
    [SerializeField] private float lifeSpan = 3f;
    [SerializeField] private float damage = 20f;
    private bool _isPlayerBullet;

    private void Start()
    {
        TimeManager.Instance.AddMultiStateObject(this);
        Destroy(gameObject, lifeSpan);
    }

    public void Setup(Vector3 shootDir, bool isPlayerBullet)
    {
        _shootDir = shootDir;
        _isPlayerBullet = isPlayerBullet;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ignore self collision
        if (_isPlayerBullet && !other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Pet"))
        {
            // if other object has health component and it is not the player, deal damage
            HealthComponent comp = other.gameObject.GetComponent<HealthComponent>();
            if (comp)
            {
                comp.TakeDamage(damage);
            }
            
            Destroy(gameObject);
        }
        else if (!_isPlayerBullet && !other.gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("Bullet"))
        {
            HealthComponent comp = other.gameObject.GetComponent<HealthComponent>();
            if (comp)
            {
                comp.TakeDamage(damage);
            }
            
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += _shootDir * (speed * Time.deltaTime);
    }

    public override void OnTimeStateChange(TimeState newTimeState)
    {
        if (newTimeState == TimeState.Past)
        {
            Destroy(gameObject);
        }
    }

    public override void OnInteract()
    {
        
    }
}
