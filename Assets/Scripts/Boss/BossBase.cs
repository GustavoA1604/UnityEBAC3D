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
    ATTACK
}

public class BossBase : MonoBehaviour
{
    [Header("Animation")]
    public float startAnimationDuration = .5f;
    public Ease startAnimationEase = Ease.OutBack;
    private StateMachine<BossAction> stateMachine;

    [Header("Attack")]
    public int attackCount = 5;
    public float timeBetweenAttacks = .5f;

    public float speed = 5f;
    public List<Transform> waypoints;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        stateMachine = new StateMachine<BossAction>();
        //stateMachine.Init();
        stateMachine.RegisterState(BossAction.INIT, new BossStateInit());
        stateMachine.RegisterState(BossAction.WALK, new BossStateWalk());
        stateMachine.RegisterState(BossAction.ATTACK, new BossStateAttack());

        SwitchState(BossAction.INIT);
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
            transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
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
