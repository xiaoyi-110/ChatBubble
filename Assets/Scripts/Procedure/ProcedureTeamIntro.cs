using System;
using UnityEngine;

public class ProcedureTeamIntro : ProcedureBase
{
    
    public enum ProcedureTeamIntroState : byte
    {
        None = 0,
        Exit,
    }
    private ProcedureTeamIntroState m_currentState;

    public override void OnEnter(FSM<ProcedureManager> fsm)
    {
        base.OnEnter(fsm);
        m_currentState = ProcedureTeamIntroState.None;

        ScenesManager.Instance.UnLoadAllScenes();

        //UIManager.Instance.OpenUIForm(Constant.UIFormData.TeamIntro, this);
    }

    public override void OnLeave(FSM<ProcedureManager> fsm)
    {
        base.OnLeave(fsm);
        //UIManager.Instance.CloseUIForm(Constant.UIFormData.TeamIntro, this);
    }

    public override void OnUpdate(FSM<ProcedureManager> fsm)
    {
        base.OnUpdate(fsm);
        switch (m_currentState)
        {
            case ProcedureTeamIntroState.Exit:
                fsm.SetData(Constant.ProcedureData.NextSceneId, Constant.SceneData.Menu);
                ChangeState<ProcedureChangeScene>(fsm);
                break;
            default:
                break;
        }
    }

    public void StartState(ProcedureTeamIntroState state)
    {
        m_currentState = state;
    }

    public static ProcedureTeamIntro Create()
    {
        ProcedureTeamIntro procedureTeamIntro = new ProcedureTeamIntro();
        return procedureTeamIntro;
    }
    
}
