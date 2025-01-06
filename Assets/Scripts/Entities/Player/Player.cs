using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : Entity
{
    [Header("Move info")]
    public float moveSpeed = 8f;
    public float jumpForce = 7.5f;

    [Header("Attack info")]
    public float comboTimeWindow = .9f;
 

    [Header("Coyote time")]
    [SerializeField]private float bufferCoyoteTimeWindow = .9f;
    public float bufferCoyoteTimer;
  

    public PlayerFSM stateMachine { get; private set; }

    #region Judge info
    public bool IsTryJump => Input.GetKeyDown(KeyCode.Space);
    public bool IsTryAttack => Input.GetKeyDown(KeyCode.Mouse0);
    public bool IsAtCoyoteTime => bufferCoyoteTimer > 0;
    #endregion



    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerFSM();

        stateMachine.AddState(typeof(PlayerIdleState), new PlayerIdleState(this, "Idle"));
        stateMachine.AddState(typeof(PlayerMoveState), new PlayerMoveState(this, "Move"));
        stateMachine.AddState(typeof(PlayerJumpState), new PlayerJumpState(this, "JumpFall"));
        stateMachine.AddState(typeof(PlayerAirState), new PlayerAirState(this, "JumpFall"));
        stateMachine.AddState(typeof(PlayerAttackState), new PlayerAttackState(this, "Attack"));

    }

    protected override void Start()
    {
        base.Start();
        stateMachine.SwitchOn(typeof(PlayerIdleState));
        
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.Update();      
    }

    public void AnimationTriggerFinished() => stateMachine.currentState.AnimationFinishTrigger();
    
    public void ResetBufferCoyoteTimer()
    {
        bufferCoyoteTimer = bufferCoyoteTimeWindow;
    }
   
    
}
