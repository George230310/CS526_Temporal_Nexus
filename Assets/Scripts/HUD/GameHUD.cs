using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textPrompt;
    [SerializeField] private Image blackOverlay;
    [SerializeField] private RectTransform optionPanel;

    public TextMeshProUGUI optionDescription;
    public TextMeshProUGUI yesButtonText;
    public TextMeshProUGUI noButtonText;
    
    public Button yesButton;
    public Button noButton;

    private float _yesButtonCost;

    public void ToggleBlackOverlay(bool enable)
    {
        blackOverlay.gameObject.SetActive(enable);
    }

    public void FadeBlackOverlay(float duration)
    {
        float half = duration / 2.0f;
        Sequence overlayFadeSequence = DOTween.Sequence();
        overlayFadeSequence.Append(blackOverlay.DOFade(1.0f, half)).Append(blackOverlay.DOFade(0.0f, half));
        
        Sequence textFadeSequence = DOTween.Sequence();
        textFadeSequence.Append(textPrompt.DOFade(1.0f, half)).Append(textPrompt.DOFade(0.0f, half));

        overlayFadeSequence.Play();
        textFadeSequence.Play();
    }

    public void SetTextPrompt(String prompt)
    {
        textPrompt.text = prompt;
    }

    public void PresentInteractionMessageOnly()
    {
        // pull out option panel
        optionPanel.DOMoveY(150f, 1f);
        
        // disable player control
        if (GameManager.Instance && GameManager.Instance.player)
        {
            GameManager.Instance.player.GetComponent<PlayerController>().enabled = false;
            GameManager.Instance.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        
        // setup option buttons
        yesButton.gameObject.SetActive(false);
        noButton.onClick.AddListener(HideInteractionMessage);
        noButtonText.text = "Close";
    }

    public void PresentInteractionMessageAndOptions(float yesCost)
    {
        // pull out option panel
        optionPanel.DOMoveY(150f, 1f);
        
        // disable player control
        if (GameManager.Instance && GameManager.Instance.player)
        {
            GameManager.Instance.player.GetComponent<PlayerController>().enabled = false;
            GameManager.Instance.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        
        // setup option buttons
        yesButton.gameObject.SetActive(true);
        
        if (yesCost > 0f)
        {
            _yesButtonCost = yesCost;
            yesButton.onClick.AddListener(DeductPlayerHealth);
            yesButtonText.text = "Yes (-" + yesCost + " HP)";
        }
        else
        {
            yesButtonText.text = "Yes";
        }
        
        yesButton.onClick.AddListener(HideInteractionMessage);
        noButton.onClick.AddListener(HideInteractionMessage);
        
    }

    public void HideInteractionMessage()
    {
        // lower option panel
        optionPanel.DOMoveY(-150f, 1f);
        
        // enable player control
        if (GameManager.Instance && GameManager.Instance.player)
        {
            GameManager.Instance.player.GetComponent<PlayerController>().enabled = true;
        }
        
        // clear all button events
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
        _yesButtonCost = 0f;
    }
    
    // deduct player health for some yes choices
    private void DeductPlayerHealth()
    {
        GameManager.Instance.player.GetComponent<HealthComponent>().TakeDamage(_yesButtonCost);
    }
}
