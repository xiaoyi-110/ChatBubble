

using System.Collections.Generic;

public class ProcedureManager : MonoSingleton<ProcedureManager>
{
    private FSM<ProcedureManager> m_FSM;
    private List<FSMState<ProcedureManager>> m_StateList;


    private void Start() {
        CreateFSM();
    }

    private void Update() {
        m_FSM.OnUpdate();
    }

    private void CreateFSM() {
        m_StateList = new List<FSMState<ProcedureManager>>()
        {
            ProcedureLaunch.Create(),
            ProcedureChangeScene.Create(),
            ProcedureMenu.Create(),
            ProcedureLevel.Create(),
            ProcedureTeamIntro.Create()
        };
    
        m_FSM = FSM<ProcedureManager>.Create(this, m_StateList);

        m_FSM.StartState<ProcedureLaunch>();
    }
}