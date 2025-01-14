using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : Entity
{
    [Header("Move info")]
    public float MoveSpeed = 8f;
    public float JumpForce = 7.5f;

    [Header("Attack info")]
    public float ComboTimeWindow = .9f;
 

    [Header("Coyote time")]
    [SerializeField]private float m_CoyoteTimeWindow = .9f;
    private float m_CoyoteTimer;
  

    #region Judge info
    public bool IsTryJump => Input.GetKeyDown(KeyCode.Space);
    public bool IsTryAttack => Input.GetKeyDown(KeyCode.Mouse0);
    public bool IsTryJumpAtCoyoteTime => IsTryJump && m_CoyoteTimer > 0;
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

    protected override void Update()
    {
        if(LevelManager.Instance.IsPause) return;
        base.Update();
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
            PlayerAttackState.Create()
        };
        m_Fsm = FSM<Player>.Create(this, m_StateList);
        m_Fsm.StartState<PlayerIdleState>();
    }

    public void AnimationTriggerFinished() => ((PlayerState)m_Fsm.CurrentState).AnimationFinishTrigger();
    
    public void ResetCoyoteTimer()
    {
        m_CoyoteTimer = m_CoyoteTimeWindow;
    }

    public void ClearCoyoteTimer()
    {
        m_CoyoteTimer = -1;
    }
   
    
}
