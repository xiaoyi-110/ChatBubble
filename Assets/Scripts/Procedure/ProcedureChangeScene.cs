
using System;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class ProcedureChangeScene : ProcedureBase
{
    private string m_SceneName;
    private bool m_IsMenuScene;
    private bool m_IsChangeSceneComplete = false;
    public override void OnEnter(FSM<ProcedureManager> fsm)
    {
        base.OnEnter(fsm);
        m_IsChangeSceneComplete = false;

        m_SceneName = fsm.GetData(Constant.ProcedureData.NextSceneId);
        m_IsMenuScene = m_SceneName == Constant.SceneData.Menu;

        EventManager.Instance.RegisterEvent(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);

        ScenesManager.Instance.UnLoadAllScenes();
        ScenesManager.Instance.LoadScene(AssetUtility.GetSceneAsset(m_SceneName), this);
    }

    public override void OnLeave(FSM<ProcedureManager> fsm)
    {
        base.OnLeave(fsm);

        EventManager.Instance.UnRegisterEvent(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
    }

    public override void OnUpdate(FSM<ProcedureManager> fsm)
    {
        base.OnUpdate(fsm);

        if (!m_IsChangeSceneComplete)
        {
            return;
        }
        
            
        if (m_IsMenuScene)
        {
            ChangeState<ProcedureMenu>(fsm);
        }
        else
        {
            ChangeState<ProcedureLevel>(fsm);
        }
        
    }

    public void OnLoadSceneSuccess(object sender, EventArgs e)
    {
        LoadSceneSuccessEventArgs ne = (LoadSceneSuccessEventArgs) e;
        if(ne.UserData != this)
        {
            return;
        }
        
        m_IsChangeSceneComplete = true;
    }

    public static ProcedureChangeScene Create()
    {
        ProcedureChangeScene procedure = new ProcedureChangeScene();
        return procedure;
    }
}