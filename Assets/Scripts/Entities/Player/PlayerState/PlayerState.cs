using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using UnityEngine;

public class PlayerState : FsmState<Player>
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


    protected internal override void OnInit(IFsm<Player> fsm)
    {
        m_Player = fsm.Owner;
        m_MoveSpeedScale = 1f;
    }

    protected internal override void OnEnter(IFsm<Player> fsm)
    {
        m_Player.Animator.SetBool(m_AnimBoolName, true);
        m_IsChanged = false;
        m_IsTriggerCalled = false;
        m_Rb = m_Player.Rb;
    }

    protected internal override void OnLeave(IFsm<Player> fsm, bool isShutdown)
    {
        m_Player.Animator.SetBool(m_AnimBoolName, false);
        m_IsChanged = true;
    }

    protected internal override void OnUpdate(IFsm<Player> fsm, float elapseSeconds, float realElapseSeconds)
    {
        m_InputHorizontal = Input.GetAxisRaw("Horizontal");
        m_Player.SetVelocity(m_MoveSpeedScale * m_InputHorizontal * m_Player.MoveSpeed, m_Rb.velocity.y);

        m_Player.Animator.SetFloat("yVelocity", m_Rb.velocity.y);
    }

    public virtual void AnimationFinishTrigger()
    {
        m_IsTriggerCalled = true;
    }

    

}
