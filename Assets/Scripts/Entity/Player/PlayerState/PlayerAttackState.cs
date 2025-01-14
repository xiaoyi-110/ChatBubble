
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private int m_ComboCounter;
    private float m_ComboTimeWindow;
    private float m_LastAttackTime;

    public PlayerAttackState(string animBoolName) : base(animBoolName)
    {
    }

    public override void OnInit(FSM<Player> fsm)
    {
        base.OnInit(fsm);
        m_MoveSpeedScale = 0;
        m_ComboCounter = 0;
    }

    public override void OnEnter(FSM<Player> fsm)
    {
        base.OnEnter(fsm);
        m_ComboTimeWindow = m_Player.ComboTimeWindow;
        if (Time.time - m_LastAttackTime >= m_ComboTimeWindow)
        {
            m_ComboCounter = 0;
        }
        m_Player.Animator.SetInteger("comboCounter", m_ComboCounter);
    }

    public override void OnLeave(FSM<Player> fsm)
    {
        base.OnLeave(fsm);
        m_ComboCounter = (m_ComboCounter + 1) % 3;
        m_LastAttackTime = Time.time;
    }

    public override void OnUpdate(FSM<Player> fsm)
    {
        base.OnUpdate(fsm);
        if(m_IsChanged) return;

        if (m_IsTriggerCalled)
        {
            ChangeState<PlayerIdleState>(fsm);
        }
    }

    public static PlayerAttackState Create()
    {
        PlayerAttackState state = new PlayerAttackState("Attack");
        return state;
    }
    

}