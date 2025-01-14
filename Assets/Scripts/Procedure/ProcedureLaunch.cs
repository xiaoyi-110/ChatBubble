
using System.Collections.Generic;

public class ProcedureLaunch : ProcedureBase
{
    private Dictionary<string, bool> m_LoadFlags;

    public override void OnInit(FSM<ProcedureManager> fsm)
    {
        base.OnInit(fsm);

        m_LoadFlags = new Dictionary<string, bool>();

    }

    public override void OnEnter(FSM<ProcedureManager> fsm)
    {
        base.OnEnter(fsm);

        m_LoadFlags.Clear();
    }

    public override void OnUpdate(FSM<ProcedureManager> fsm)
    {
        base.OnUpdate(fsm);

        foreach(KeyValuePair<string, bool> loadFlag in m_LoadFlags)
        {
            if (!loadFlag.Value)
            {
                return;
            }
        }


        fsm.SetData(Constant.ProcedureData.NextSceneId, Constant.SceneData.Menu);
        ChangeState<ProcedureChangeScene>(fsm);
    }

    public static ProcedureLaunch Create()
    {
        ProcedureLaunch procedure = new ProcedureLaunch();
        return procedure;
    }
}