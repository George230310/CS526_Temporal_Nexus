using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MultiStateObjectComponent
{
    private bool _isSaved;
    private bool _canBeSaved;
    private bool _isAttachedToPlayer;

    [SerializeField] private GameObject[] sprites;
    [SerializeField] private float shootCoolDown = 0.5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float saveCost;
    private float _shootTimer = 0.0f;

    private void Update()
    {
        // shooting enemy logic
        if (_isAttachedToPlayer)
        {
            _shootTimer += Time.deltaTime;
            if (_shootTimer >= shootCoolDown)
            {
                _shootTimer = 0.0f;

                // if there is a target to shoot and the target is present.
                if (GameManager.Instance.closestEnemyInPetRange && GameManager.Instance.closestEnemyInPetRange.activeSelf)
                {
                    // fire projectile
                    Vector3 shootDir = GameManager.Instance.closestEnemyInPetRange.transform.position - gameObject.transform.position;
                    shootDir.Normalize();

                    GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                    bullet.GetComponent<Bullet>().Setup(shootDir, true);
                }
            }
        }
    }

    private void SavePet()
    {
        if (_canBeSaved)
        {
            _isSaved = true;
            // Do not interact with the saved pet in the past.
            if (TimeManager.Instance.CurrentGlobalTimeState == TimeState.Past)
            {
                isInteractable = false;
            }
            
            AttachPetToPlayer();
        }
    }

    public override void OnTimeStateChange(TimeState newTimeState)
    {
        switch (newTimeState)
        {
            case TimeState.Past:
                _canBeSaved = true;
                SetPetSprite(2);

                break;

            case TimeState.Present:
                _canBeSaved = false;

                // The pet is not interactable only if attached to the player.
                if (_isAttachedToPlayer)
                {
                    isInteractable = false;
                }
                else
                {
                    isInteractable = true;
                }

                // set the sprite for the pet based on conditions
                SetPetSprite(_isSaved ? 2 : 0);

                break;
        }
    }

    // function to set the sprite of this pet
    private void SetPetSprite(int idx)
    {
        // do nothing if the pet is already attached to player
        if (_isAttachedToPlayer)
        {
            return;
        }

        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].SetActive(i == idx);
        }
    }

    public override void OnInteract()
    {
        // if the pet is not saved, save the pet
        if (!_isSaved)
        {
            if (TimeManager.Instance.CurrentGlobalTimeState == TimeState.Past)
            {
                SavePet();
            }
        }
    }

    private void AttachPetToPlayer()
    {
        // attach to the player
        GameObject player = GameManager.Instance.player;
        Transform attachmentPointTransform = player.transform.Find("PetAttachmentPoint");

        if (attachmentPointTransform)
        {
            gameObject.transform.position = attachmentPointTransform.position;
            gameObject.transform.SetParent(attachmentPointTransform);
            _isAttachedToPlayer = true;
            isInteractable = false;
        }
    }
}
