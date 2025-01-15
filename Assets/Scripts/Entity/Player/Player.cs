using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    [Header("Move info")]
    public float MoveSpeed = 8f;
    public float JumpForce = 7.5f;
    public float SprintSpeed = 20f;

    [Header("Attack info")]
    public float ComboTimeWindow = .9f;
 

    [Header("Time Window")]
    [SerializeField]private float m_CoyoteTimeWindow = .9f;
    public float CoyoteTimer;
    [SerializeField]private float m_SprintTimeWindow = 2f;
    public float SprintTimer;
  

    #region Judge info
    public bool IsTryJump => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W);
    public bool IsTrySprint => (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Mouse1)) && SprintTimer <= 0;
    public bool IsTryAttack => Input.GetKeyDown(KeyCode.Mouse0);
    public bool IsTryJumpAtCoyoteTime => IsTryJump && CoyoteTimer > 0;
    
    #endregion

    internal FSM<Player> m_Fsm;
    private List<FSMState<Player>> m_StateList;


    protected override void Awake()
    {
        base.Awake();
        
        
    }

    protected override void Start()
    {
        base.Start();
        
        CreateFSM();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        m_Fsm.OnUpdate();  
    }

    private void CreateFSM()
    {
        m_StateList = new List<FSMState<Player>>()
        {
            PlayerIdleState.Create(),
            PlayerMoveState.Create(),
            PlayerJumpState.Create(),
            PlayerAirState.Create(),
            PlayerAttackState.Create(),
            PlayerSprintState.Create()
        };
        m_Fsm = FSM<Player>.Create(this, m_StateList);
        m_Fsm.StartState<PlayerIdleState>();
    }

    public void AnimationTriggerFinished() => ((PlayerState)m_Fsm.CurrentState).AnimationFinishTrigger();

    public void ResetSprintTimer()
    {
        SprintTimer = m_SprintTimeWindow;
    }

    public void ResetCoyoteTimer()
    {
        CoyoteTimer = m_CoyoteTimeWindow;
    }

    public void ClearCoyoteTimer()
    {
        CoyoteTimer = -1;
    }
   
    
}
