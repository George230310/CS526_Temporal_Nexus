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
                
                // if there is a target to shoot
                if (GameManager.Instance.closestEnemyInPetRange)
                {
                    // fire projectile
                    Vector3 shootDir = GameManager.Instance.closestEnemyInPetRange.transform.position - gameObject.transform.position;
                    shootDir.Normalize();

                    GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                    bullet.GetComponent<Bullet>().Setup(shootDir);
                }
            }
        }
    }

    private void SavePet()
    {
        if (_canBeSaved)
        {
            _isSaved = true;
        }
    }

    public override void OnTimeStateChange(TimeState newTimeState)
    {
        switch (newTimeState)
        {
            case TimeState.Past:
                _canBeSaved = true;
                SetPetSprite(1);
                
                break;
            
            case TimeState.Present:
                _canBeSaved = false;
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
            if (TimeManager.Instance.CurrentGlobalTimeState == TimeState.Present)
            {
                GameManager.Instance.gameHUD.optionDescription.text = "This looks like skeleton of some animal that died a long time ago...";
                GameManager.Instance.gameHUD.PresentInteractionMessageOnly();
            }
            else if (TimeManager.Instance.CurrentGlobalTimeState == TimeState.Past)
            {
                GameManager.Instance.gameHUD.optionDescription.text = "This small injured Slime is looking at you and want to be helped...";
                GameManager.Instance.gameHUD.yesButton.onClick.AddListener(SavePet);
                GameManager.Instance.gameHUD.PresentInteractionMessageAndOptions(saveCost);
            }
        }
        // else if the pet is saved and the time is at present
        else if (TimeManager.Instance.CurrentGlobalTimeState == TimeState.Present)
        {
            GameManager.Instance.gameHUD.optionDescription.text = "The Slime you saved has grown bigger. It recognizes you and want to follow you.";
            GameManager.Instance.gameHUD.yesButton.onClick.AddListener(AttachPetToPlayer);
            GameManager.Instance.gameHUD.PresentInteractionMessageAndOptions(0.0f);
        }
    }

    private void AttachPetToPlayer()
    {
        // attach to the player
        GameObject player = GameManager.Instance.player;
        Transform attachmentPointTransform = player.transform.Find("PetAttachmentPoint");

        gameObject.transform.position = attachmentPointTransform.position;
        gameObject.transform.SetParent(attachmentPointTransform);
        _isAttachedToPlayer = true;
    }
}
