using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiStateProjectileEnemy : MultiStateEnemy
{
    private EnemyDamageDealer _dealer;
    private EnemyMovement _movement;
    private Rigidbody2D _rigidbody;

    [SerializeField] private GameObject enemySprite;
    [SerializeField] private GameObject enemyEggSprite;
    [SerializeField] private float shootCoolDown = 0.5f;
    private float _shootTimer;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int fireAngleMin = 0;
    [SerializeField] private int fireAngleMax = 180;
    [SerializeField] private int fireAngleOffset = 30;
    
    private void Awake()
    {
        _dealer = GetComponent<EnemyDamageDealer>();
        _movement = GetComponent<EnemyMovement>();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // if it the present, fire projectiles
        if (TimeManager.Instance.CurrentGlobalTimeState == TimeState.Present)
        {
            _shootTimer += Time.deltaTime;
            if (_shootTimer >= shootCoolDown)
            {
                _shootTimer = 0.0f;
                
                // fire
                for (int fireAngle = fireAngleMin; fireAngle <= fireAngleMax; fireAngle += fireAngleOffset)
                {
                    Vector3 rotatedVector = Quaternion.AngleAxis(fireAngle, Vector3.forward) * Vector3.right;
                    GameObject enemyBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                    enemyBullet.GetComponent<Bullet>().Setup(rotatedVector, false);
                }
            }
        }
    }

    public override void OnTimeStateChange(TimeState newTimeState)
    {
        switch (newTimeState)
        {
            case TimeState.Past:
                if (_dealer)
                {
                    // egg cannot be stomped in the past
                    _dealer.ToggleCanBeStomped(false);
                    
                    // egg cannot deal damage in the past
                    _dealer.ToggleCanDealDamage(false);
                }
                
                // switch to egg sprite
                enemyEggSprite.SetActive(true);
                enemySprite.SetActive(false);

                if (_movement)
                {
                    _movement.enabled = false;
                }

                _rigidbody.isKinematic = true;
                _rigidbody.velocity = Vector2.zero;
                
                break;
            
            case TimeState.Present:
                if (_dealer)
                {
                    // enemy can be stomped in the present
                    _dealer.ToggleCanBeStomped(true);
                    
                    // enemy can deal damage in the present
                    _dealer.ToggleCanDealDamage(true);
                }
                
                // switch to enemy sprite
                enemySprite.SetActive(true);
                enemyEggSprite.SetActive(false);
                
                if (_movement)
                {
                    _movement.enabled = true;
                }

                _rigidbody.isKinematic = false;
                _rigidbody.velocity = Vector2.zero;
                
                break;
        }
    }

    public override void OnInteract()
    {
        
    }
}
