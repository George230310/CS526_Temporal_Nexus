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
}
