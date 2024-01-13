using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public static Player _instance;

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
    private bool _previousIsGrounded;

    [Header("Animation")]
    public Animator animator;
    public float goingDownSpeedAnimationThreshold = 1f;

    [Header("Life and Damage")]
    public List<FlashColor> flashColors;
    public HealthBase healthBase;

    [Header("Respawn")]
    public float timeToRespawn = 2f;
    [Header("Cloth")]
    public ClothChanger clothChanger;

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
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        healthBase.OnDamage += OnPlayerDamage;
        healthBase.OnKill += OnPlayerKill;
        _previousIsGrounded = characterController.isGrounded;
    }

    void Update()
    {
        animator.SetBool("JumpStarted", false);
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
                animator.SetBool("JumpStarted", true);
            }
        }

        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        animator.SetBool("JumpFinished", characterController.isGrounded && !_previousIsGrounded);
        animator.SetBool("JumpGoingDown", !characterController.isGrounded && vSpeed < -goingDownSpeedAnimationThreshold);

        _previousIsGrounded = characterController.isGrounded;
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

    public void ChangeSpeed(float speedMultiplier, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speedMultiplier, duration));
    }

    private IEnumerator ChangeSpeedCoroutine(float speedMultiplier, float duration)
    {
        speed *= speedMultiplier;
        yield return new WaitForSeconds(duration);
        speed /= speedMultiplier;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    private IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        clothChanger.ChangeTexture(setup);
        yield return new WaitForSeconds(duration);
        clothChanger.ResetTexture();
    }
}
