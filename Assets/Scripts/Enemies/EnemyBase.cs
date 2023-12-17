using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBase : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    [SerializeField] private float _currentLife;

    [Header("Animation")]
    [SerializeField] private AnimationBase _animationBase;
    public float startAnimationDuration = .2f;
    public Ease startAnimationEase = Ease.OutBack;
    public bool startWithBornAnimation = true;

    private void Awake()
    {
        Init();
    }

    protected void ResetLife()
    {
        _currentLife = startLife;
    }
    protected virtual void Init()
    {
        ResetLife();
        if (startWithBornAnimation)
        {
            BornAnimation();
        }
    }
    protected virtual void Kill()
    {
        OnKill();
    }

    protected virtual void OnKill()
    {
        Destroy(gameObject, 3f);
        PlayAnimationByTrigger(AnimationType.DEATH);
    }

    public void OnDamage(float f)
    {
        if (_currentLife > 0)
        {
            _currentLife -= f;
            if (_currentLife <= 0)
            {
                Kill();
            }
        }
    }

    private void BornAnimation()
    {
        transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
    }

    public void PlayAnimationByTrigger(AnimationType animationType)
    {
        _animationBase.PlayAnimationByTrigger(animationType);
    }

    public void Damage(float damage)
    {
        OnDamage(damage);
    }
}
