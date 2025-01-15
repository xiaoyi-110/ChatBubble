using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : FSMState<Player>
{
    protected bool m_IsChanged;
    protected Player m_Player;
    protected Rigidbody2D m_Rb;

    protected float m_InputHorizontal;
    protected float m_MoveSpeedScale;
    protected bool m_IsTriggerCalled;
    private readonly string m_AnimBoolName;

    public PlayerState(string animBoolName) 
    { 
        m_AnimBoolName = animBoolName;
    }


    public override void OnInit(FSM<Player> fsm)
    {
        m_Player = fsm.Owner;
        m_MoveSpeedScale = 1f;
    }

    public override void OnEnter(FSM<Player> fsm)
    {
        m_Player.Animator.SetBool(m_AnimBoolName, true);
        m_IsChanged = false;
        m_IsTriggerCalled = false;
        m_Rb = m_Player.Rb;
    }

    public override void OnLeave(FSM<Player> fsm)
    {
        m_Player.Animator.SetBool(m_AnimBoolName, false);
        m_IsChanged = true;
    }

    public override void OnUpdate(FSM<Player> fsm)
    {
        m_InputHorizontal = Input.GetAxisRaw("Horizontal");
        m_Player.SetVelocity(m_MoveSpeedScale * m_InputHorizontal * m_Player.MoveSpeed, m_Rb.velocity.y);

        m_Player.Animator.SetFloat("yVelocity", m_Rb.velocity.y);

        if(m_Player.CoyoteTimer >= 0)
        {
            m_Player.CoyoteTimer -= Time.deltaTime;
        }
        if(m_Player.SprintTimer >= 0)
        {
            m_Player.SprintTimer -= Time.deltaTime;
        }

        if(m_Player.IsTrySprint)
        {
            ChangeState<PlayerSprintState>(fsm);
            m_IsChanged = true;
        }
    }

    public virtual void AnimationFinishTrigger()
    {
        m_IsTriggerCalled = true;
    }

    

}
