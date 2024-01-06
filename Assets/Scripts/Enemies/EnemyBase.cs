using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBase : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    [SerializeField] private float _currentLife;
    public FlashColor flashColor;
    public ParticleSystem myParticleSystem;

    [Header("Animation")]
    [SerializeField] private AnimationBase _animationBase;
    public float startAnimationDuration = .2f;
    public Ease startAnimationEase = Ease.OutBack;
    public bool startWithBornAnimation = true;
    public float timeToDisappearOnKill = 1.5f;

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
        Destroy(gameObject, timeToDisappearOnKill);
        PlayAnimationByTrigger(AnimationType.DEATH);
    }

    public void OnDamage(float f)
    {
        if (_currentLife > 0)
        {
            flashColor?.Flash();
            myParticleSystem?.Emit(15);
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

    public void Damage(float damage, Vector3 dir)
    {
        dir.y = 0;
        dir = dir.normalized;
        transform.DOMove(transform.position - dir, .1f);
        OnDamage(damage);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player p = collision.transform.GetComponent<Player>();
        if (p != null)
        {
            p.Damage(1);
        }
    }

    public virtual void Update()
    {
    }
}
