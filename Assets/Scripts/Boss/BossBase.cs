using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum BossAction
{
    INIT,
    IDLE,
    WALK,
    ATTACK,
    DEATH
}

public class BossBase : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private AnimationBase _animationBase;
    public float startAnimationDuration = .5f;
    public Ease startAnimationEase = Ease.OutBack;
    private StateMachine<BossAction> stateMachine;
    public float timeToDisappearOnKill = 2f;

    [Header("Attack")]
    public int attackCount = 5;
    public float timeBetweenAttacks = .5f;

    public HealthBase healthBase;

    public float speed = 5f;
    public List<Transform> waypoints;
    public GunBase gunBase;
    public FlashColor flashColor;
    public ParticleSystem myParticleSystem;


    private void OnValidate()
    {
        if (healthBase == null)
        {
            healthBase = GetComponent<HealthBase>();
        }
        if (flashColor == null)
        {
            flashColor = GetComponent<FlashColor>();
        }
        if (_animationBase == null)
        {
            _animationBase = GetComponent<AnimationBase>();
        }
    }

    private void Awake()
    {
        Init();
        healthBase.OnKill += OnBossKill;
        healthBase.OnDamage += OnBossDamage;
    }

    private void Init()
    {
        stateMachine = new StateMachine<BossAction>();
        //stateMachine.Init();
        stateMachine.RegisterState(BossAction.INIT, new BossStateInit());
        stateMachine.RegisterState(BossAction.WALK, new BossStateWalk());
        stateMachine.RegisterState(BossAction.ATTACK, new BossStateAttack());
        stateMachine.RegisterState(BossAction.DEATH, new BossStateDeath());

        SwitchState(BossAction.INIT);
    }

    private void OnBossKill(HealthBase h)
    {
        SwitchState(BossAction.DEATH);
        gunBase.StopShoot();
        _animationBase.PlayAnimationByTrigger(AnimationType.DEATH);
        Destroy(gameObject, timeToDisappearOnKill);
    }

    public void OnBossDamage(HealthBase healthBase)
    {
        flashColor?.Flash();
        myParticleSystem?.Emit(15);
    }

    public void StartAttack(Action endCallback = null)
    {
        StartCoroutine(AttackCoroutine(endCallback));
    }

    IEnumerator AttackCoroutine(Action endCallback = null)
    {
        int attacks = 0;
        while (attacks < attackCount)
        {
            attacks++;
            gunBase.Shoot();
            _animationBase.PlayAnimationByTrigger(AnimationType.ATTACK);
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
        endCallback?.Invoke();
    }

    public void GotoRandomPoint(Action onArrive = null)
    {
        StartCoroutine(GoToPointCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
    }

    IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null)
    {
        while (Vector3.Distance(transform.position, t.position) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
        onArrive?.Invoke();
    }

    public void StartInitAnimation()
    {
        transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        StartCoroutine(SwitchStateAfter(BossAction.WALK, startAnimationDuration));
    }

    private IEnumerator SwitchStateAfter(BossAction state, float duration)
    {
        yield return new WaitForSeconds(duration);
        SwitchState(state);
    }

    public void SwitchState(BossAction state)
    {
        stateMachine.SwitchState(state, this);
    }

    [NaughtyAttributes.Button]
    public void SwitchToWalk()
    {
        SwitchState(BossAction.WALK);
    }
}
