using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DestructableItemBase : MonoBehaviour
{
    public HealthBase healthBase;
    private Vector3 _originalScale;
    public float shareDuration = .1f;
    public int shakeForce = 1;

    private void OnValidate()
    {
        if (healthBase == null)
        {
            healthBase = GetComponent<HealthBase>();
        }
    }

    private void Awake()
    {
        OnValidate();
        healthBase.OnDamage += OnDamage;
        _originalScale = transform.localScale;
    }

    private void OnDamage(HealthBase h)
    {
        transform.DOKill();
        transform.localScale = _originalScale;
        transform.DOShakeScale(shareDuration, Vector3.up * .2f, shakeForce);
    }
}
