
using GameFramework.Fsm;
using Mono.CompilerServices.SymbolWriter;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private int m_ComboCounter;
    private float m_ComboTimeWindow;
    private float m_LastAttackTime;

    public PlayerAttackState(string animBoolName) : base(animBoolName)
    {
    }

    protected internal override void OnInit(IFsm<Player> fsm)
    {
        base.OnInit(fsm);
        m_MoveSpeedScale = 0;
        m_ComboCounter = 0;
    }

    protected internal override void OnEnter(IFsm<Player> fsm)
    {
        base.OnEnter(fsm);
        m_ComboTimeWindow = m_Player.ComboTimeWindow;
        if (Time.time - m_LastAttackTime >= m_ComboTimeWindow)
        {
            m_ComboCounter = 0;
        }
        m_Player.Animator.SetInteger("comboCounter", m_ComboCounter);
    }

    protected internal override void OnLeave(IFsm<Player> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        m_ComboCounter = (m_ComboCounter + 1) % 3;
        m_LastAttackTime = Time.time;
    }

    protected internal override void OnUpdate(IFsm<Player> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if(m_IsChanged) return;

        if (m_IsTriggerCalled)
        {
            ChangeState<PlayerIdleState>(fsm);
        }
    }

}