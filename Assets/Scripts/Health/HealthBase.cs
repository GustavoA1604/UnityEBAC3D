using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    public bool destroyOnKill = false;
    public float knockback = 1f;
    [SerializeField] private float _currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;
    public List<UIFillUpdater> uIFillUpdaters;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        ResetLife();
    }
    protected void ResetLife()
    {
        _currentLife = startLife;
    }
    protected virtual void Kill()
    {
        if (destroyOnKill)
        {
            Destroy(gameObject, 3f);
        }
        OnKill?.Invoke(this);
    }

    public void Damage(float f)
    {
        if (_currentLife > 0)
        {
            _currentLife -= f;
            UpdateUI();
            if (_currentLife <= 0)
            {
                Kill();
            }
            OnDamage?.Invoke(this);
        }
    }

    public void Damage(float damage, Vector3 dir)
    {
        if (knockback != 0)
        {
            dir.y = 0;
            dir = dir.normalized;
            transform.DOMove(transform.position + dir * knockback, .1f);
        }
        Damage(damage);
    }

    public bool IsDead()
    {
        return _currentLife <= 0;
    }

    private void UpdateUI()
    {
        uIFillUpdaters.ForEach(i => i.UpdateValue(startLife, _currentLife));
    }
}
