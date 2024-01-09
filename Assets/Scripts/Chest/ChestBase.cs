using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChestBase : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.Z;
    public Animator animator;
    public string animatorTriggerOpen = "Open";
    [Header("Notification")]
    public GameObject notification;
    public float tweenDuration = .2f;
    public Ease tweenEase = Ease.OutBack;
    private Vector3 startScale;
    private bool _isPlayerClose = false;
    private bool _isChestOpened = false;

    private void OnValidate()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void Start()
    {
        startScale = notification.transform.localScale;
        notification.transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode) && _isPlayerClose && !_isChestOpened)
        {
            OpenChest();
        }
    }

    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        animator.SetTrigger(animatorTriggerOpen);
        _isChestOpened = true;
        HideNotification();
    }

    public void OnTriggerEnter(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();
        if (p != null)
        {
            ShowNotification();
            _isPlayerClose = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();
        if (p != null)
        {
            HideNotification();
            _isPlayerClose = false;
        }
    }


    private void ShowNotification()
    {
        notification.transform.DOKill();
        notification.transform.DOScale(startScale, tweenDuration).SetEase(tweenEase);
    }

    private void HideNotification()
    {
        notification.transform.DOKill();
        notification.transform.DOScale(0f, tweenDuration).SetEase(tweenEase);
    }
}
