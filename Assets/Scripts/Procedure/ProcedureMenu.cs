public class ProcedureMenu : ProcedureBase
{

    private bool m_StartGame = false;

    public override void OnEnter(FSM<ProcedureManager> fsm)
    {
        base.OnEnter(fsm);

        m_StartGame = false;

        UIManager.Instance.OpenUIForm(Constant.UIFormData.Menu, this);
        //UIManager.Instance.OpenUIForm(
    }

    public override void OnLeave(FSM<ProcedureManager> fsm)
    {
        base.OnLeave(fsm);

        UIManager.Instance.CloseUIForm(Constant.UIFormData.Menu, this);
    }

    public override void OnUpdate(FSM<ProcedureManager> fsm)
    {
        base.OnUpdate(fsm);


        if (m_StartGame)
        {
            fsm.SetData(Constant.ProcedureData.NextSceneId, Constant.SceneData.Level1);
            ChangeState<ProcedureChangeScene>(fsm);
            
        }
    }

    public void StartGame()
    {
        m_StartGame = true;
    }

    public static ProcedureMenu Create()
    {
        ProcedureMenu procedure = new ProcedureMenu();
        return procedure;
    }
}