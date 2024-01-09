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

    public int maxNumberOfCoinsToDrop = 10;
    private int _numberOfCoinsDropped = 0;
    public float dropChance = .5f;
    public GameObject coinPrefab;
    public Transform dropPosition;


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
        if (Random.Range(0f, 1f) < dropChance)
        {
            DropCoins();
        }
    }

    private void DropCoins()
    {
        if (_numberOfCoinsDropped >= maxNumberOfCoinsToDrop)
        {
            return;
        }

        _numberOfCoinsDropped++;
        var i = Instantiate(coinPrefab);
        i.transform.position = dropPosition.position;
        i.transform.DOScale(0f, 1f).SetEase(Ease.OutBack).From();
        Vector3 initialVelocity = Vector3.forward * Random.Range(-.1f, .1f) + Vector3.right * Random.Range(-.1f, .1f);
        initialVelocity /= initialVelocity.magnitude;
        initialVelocity *= 2f;
        initialVelocity += Vector3.up * 10f;
        i.GetComponent<Rigidbody>().velocity = initialVelocity;

    }
}
