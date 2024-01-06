using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateBase : StateBase
{
    protected PlayerBase _player;
    protected Rigidbody _playerRigidbody;

    public PlayerStateBase(PlayerBase player)
    {
        _player = player;
        _playerRigidbody = player.GetComponent<Rigidbody>();
    }

    protected float GetForwardSpeedFromInput()
    {
        float speed = 0f;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            speed += 10f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            speed -= 10f;
        }
        return speed;
    }

    protected Vector3 GetRotationFromInput()
    {
        float rotation = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotation += 3;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotation -= 3;
        }
        return new Vector3(0, rotation, 0);
    }

    protected void UpdateVelocityAndRotationFromInput()
    {
        Vector3 inputVelocity = _player.transform.forward * GetForwardSpeedFromInput();
        Vector3 horizontalVelocity = new Vector3(inputVelocity.x + _playerRigidbody.velocity.x, 0, inputVelocity.z + _playerRigidbody.velocity.z);
        if (horizontalVelocity.magnitude > 10)
        {
            horizontalVelocity = horizontalVelocity * 10 / horizontalVelocity.magnitude;
        }
        Vector3 finalVelocity = new Vector3(horizontalVelocity.x, _playerRigidbody.velocity.y, horizontalVelocity.z);
        _playerRigidbody.velocity = finalVelocity;
        _player.transform.Rotate(GetRotationFromInput());
    }
}

public class PlayerStateIdle : PlayerStateBase
{
    public PlayerStateIdle(PlayerBase player) : base(player) { }

    public override void OnStateStay(params object[] objs)
    {
        UpdateVelocityAndRotationFromInput();
    }
}

public class PlayerStateWalking : PlayerStateBase
{
    public PlayerStateWalking(PlayerBase player) : base(player) { }

    public override void OnStateStay(params object[] objs)
    {
        UpdateVelocityAndRotationFromInput();
    }
}

public class PlayerStateJumping : PlayerStateBase
{
    public PlayerStateJumping(PlayerBase player) : base(player) { }

}

public class PlayerBase : MonoBehaviour
{
    public enum PlayerStates
    {
        IDLE,
        WALKING,
        JUMPING
    }

    public StateMachine<PlayerStates> stateMachine;
    private Rigidbody _rigidbody;
    private PlayerStates _currentState;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Init();
    }

    public void Init()
    {
        stateMachine = new StateMachine<PlayerStates>();
        stateMachine.RegisterState(PlayerStates.IDLE, new PlayerStateIdle(this));
        stateMachine.RegisterState(PlayerStates.WALKING, new PlayerStateWalking(this));
        stateMachine.RegisterState(PlayerStates.JUMPING, new PlayerStateJumping(this));
        stateMachine.SwitchState(PlayerStates.IDLE);
        _currentState = PlayerStates.IDLE;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 newVelocity = new Vector3(_rigidbody.velocity.x, 10, _rigidbody.velocity.z);
            _rigidbody.velocity = newVelocity;
            stateMachine.SwitchState(PlayerStates.JUMPING);
            _currentState = PlayerStates.JUMPING;
        }
        else
        {
            Vector3 horizontalVelocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            if (horizontalVelocity.magnitude > 0.05)
            {
                if (_currentState == PlayerStates.IDLE)
                {
                    stateMachine.SwitchState(PlayerStates.WALKING);
                    _currentState = PlayerStates.WALKING;
                }
            }
            else
            {
                if (_currentState == PlayerStates.WALKING)
                {
                    stateMachine.SwitchState(PlayerStates.IDLE);
                    _currentState = PlayerStates.IDLE;
                }
            }
        }
        stateMachine.Update();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_currentState == PlayerStates.JUMPING)
        {
            stateMachine.SwitchState(PlayerStates.WALKING);
            _currentState = PlayerStates.WALKING;
        }
    }
}
