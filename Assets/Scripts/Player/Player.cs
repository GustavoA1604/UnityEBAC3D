using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController characterController;
    public HealthBase healthBase;
    public List<Collider> colliders;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;
    private float vSpeed = 0f;
    public float jumpSpeed = 15f;
    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    public Animator animator;

    [Header("Flash")]
    public List<FlashColor> flashColors;

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
    }

    private void OnPlayerKill(HealthBase h)
    {
        animator.SetTrigger("Death");
        SetCollidersEnabled(false);
    }

    private void SetCollidersEnabled(bool e)
    {
        colliders.ForEach(i => i.enabled = e);
    }
}
