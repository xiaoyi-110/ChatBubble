
using System;

public class ProcedureLevel : ProcedureBase
{
    private ProcedureLevelState m_ProcedureLevelState;
    public enum ProcedureLevelState : byte   
    {
        None = 0,
        BackToMenu
    }
    public override void OnInit(FSM<ProcedureManager> fsm)
    {
        base.OnInit(fsm);
    }


    public override void OnEnter(FSM<ProcedureManager> fsm)
    {
        base.OnEnter(fsm);
        m_ProcedureLevelState = ProcedureLevelState.None;

        LevelManager.Instance.InitLevel();
        UIManager.Instance.OpenUIForm(Constant.UIFormData.Level, this);
    }


    public override void OnLeave(FSM<ProcedureManager> fsm)
    {
        base.OnLeave(fsm);

        LevelManager.Instance.ExitLevel();
        UIManager.Instance.CloseUIForm(Constant.UIFormData.Level, this);
    }

    public override void OnUpdate(FSM<ProcedureManager> fsm)
    {   
        base.OnUpdate(fsm);


        switch (m_ProcedureLevelState)
        {
            case ProcedureLevelState.BackToMenu:
                fsm.SetData(Constant.ProcedureData.NextSceneId, Constant.SceneData.Menu);
                ChangeState<ProcedureChangeScene>(fsm);
                break;
            default:
                break;
        }
    }

    public static ProcedureLevel Create()
    {
        ProcedureLevel procedure = new ProcedureLevel();
        return procedure;
    }

    public void BackToMenu()
    {
        m_ProcedureLevelState = ProcedureLevelState.BackToMenu;
    }
}