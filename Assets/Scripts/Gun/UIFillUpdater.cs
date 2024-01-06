using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIFillUpdater : MonoBehaviour
{
    public Image uiImage;

    [Header("Animation")]
    public float baseAnimationDuration = 0.05f;
    public float distanceAnimationMultiplier = 0.5f;
    public Ease ease = Ease.InOutSine;

    private void OnValidate()
    {
        if (uiImage == null)
        {
            uiImage = GetComponent<Image>();
        }
    }

    public void UpdateValue(float f)
    {
        float duration = Math.Abs(uiImage.fillAmount - f) * distanceAnimationMultiplier + baseAnimationDuration;
        uiImage.DOKill();
        uiImage.DOFillAmount(f, duration).SetEase(ease);
    }

    public void UpdateValue(float max, float current)
    {
        UpdateValue(current / max);
    }
}
