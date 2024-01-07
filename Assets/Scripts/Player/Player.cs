using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public CharacterController characterController;
    public List<Collider> colliders;
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;
    private float vSpeed = 0f;
    public float jumpSpeed = 15f;

    [Header("Animation")]
    public Animator animator;

    [Header("Life and Damage")]
    public List<FlashColor> flashColors;
    public HealthBase healthBase;

    [Header("Respawn")]
    public float timeToRespawn = 2f;

    private void OnValidate()
    {
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
        if (healthBase == null)
        {
            healthBase = GetComponent<HealthBase>();
        }
    }

    private void Awake()
    {
        healthBase.OnDamage += OnPlayerDamage;
        healthBase.OnKill += OnPlayerKill;
    }

    void Update()
    {
        if (healthBase.IsDead())
        {
            return;
        }

        if (!healthBase.IsDead())
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
        }

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = healthBase.IsDead() ? Vector3.zero : transform.forward * inputAxisVertical * speed;

        var isWalking = !healthBase.IsDead() && inputAxisVertical != 0;
        if (isWalking)
        {
            if (Input.GetKey(keyRun))
            {
                speedVector *= speedRun;
                animator.speed = speedRun;
            }
            else
            {
                animator.speed = 1;
            }
        }

        if (characterController.isGrounded)
        {
            vSpeed = 0;
            if (!healthBase.IsDead() && Input.GetKey(KeyCode.Space))
            {
                vSpeed = jumpSpeed;
            }
        }

        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        characterController.Move(speedVector * Time.deltaTime);

        animator.SetBool("Run", inputAxisVertical != 0);
    }

    private void OnPlayerDamage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.FlashColorVignette();
        ShakeCamera.Shake(3f, 1f, .3f);
    }

    private void OnPlayerKill(HealthBase h)
    {
        animator.SetTrigger("Death");
        SetCollidersEnabled(false);
        Invoke(nameof(Respawn), timeToRespawn);
    }

    private void SetCollidersEnabled(bool e)
    {
        colliders.ForEach(i => i.enabled = e);
    }

    private IEnumerator SetCollidersEnabledAfterTime(bool e, float time)
    {
        yield return new WaitForSeconds(time);
        SetCollidersEnabled(e);
    }

    public void Respawn()
    {
        healthBase.Init();
        animator.SetTrigger("Respawn");
        if (CheckpointManager.HasCheckpoint())
        {
            transform.DOKill();
            transform.position = CheckpointManager.GetRespawnPosition();
        }
        StartCoroutine(SetCollidersEnabledAfterTime(true, .05f));
    }
}
