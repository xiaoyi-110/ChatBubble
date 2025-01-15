using System;
using JetBrains.Annotations;

public class ProcedureMenu : ProcedureBase
{

    public enum ProcedureMenuState : byte
    {
        None = 0,
        StartGame,
        StartTeamIntro,   
    }

    private ProcedureMenuState m_currentState;
    

    public override void OnEnter(FSM<ProcedureManager> fsm)
    {
        base.OnEnter(fsm);

        m_currentState = ProcedureMenuState.None;

        UIManager.Instance.OpenUIForm(Constant.UIFormData.Menu, this);
        
    }

    public override void OnLeave(FSM<ProcedureManager> fsm)
    {
        base.OnLeave(fsm);

        UIManager.Instance.CloseUIForm(Constant.UIFormData.Menu, this);
    }

    public override void OnUpdate(FSM<ProcedureManager> fsm)
    {
        base.OnUpdate(fsm);


        switch (m_currentState)
        {
            case ProcedureMenuState.StartGame:    
                fsm.SetData(Constant.ProcedureData.NextSceneId, Constant.SceneData.Level1);
                ChangeState<ProcedureChangeScene>(fsm);
                break;
            case ProcedureMenuState.StartTeamIntro:
                ChangeState<ProcedureTeamIntro>(fsm);
                break;
            default:
                break;
        }
    }

    public void StartState(ProcedureMenuState state)
    {
        m_currentState = state;
    }



    public static ProcedureMenu Create()
    {
        ProcedureMenu procedure = new ProcedureMenu();
        return procedure;
    }
}