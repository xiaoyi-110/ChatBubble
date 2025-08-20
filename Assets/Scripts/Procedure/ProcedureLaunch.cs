using System.Collections.Generic;
using UnityEngine;

public class ProcedureLaunch : ProcedureBase
{
    private Dictionary<string, bool> m_LoadFlags;
    private FSM<ProcedureManager> m_FSM; 

    public override void OnInit(FSM<ProcedureManager> fsm)
    {
        base.OnInit(fsm);
        m_LoadFlags = new Dictionary<string, bool>();
    }

    public override void OnEnter(FSM<ProcedureManager> fsm)
    {
        base.OnEnter(fsm);

        m_FSM = fsm;

        m_LoadFlags.Clear();
        AssetManager.Instance.LoadInitialAssetsAsync(OnAssetsLoaded);
    }

    public override void OnUpdate(FSM<ProcedureManager> fsm)
    {
       
    }
    private void OnAssetsLoaded()
    {
        SoundDatabase loadedDatabase = AssetManager.Instance.LoadedSoundDatabase;
        if (loadedDatabase != null)
        {
            // 将加载好的数据库传递给 AudioManager
            AudioManager.Instance.SetSoundDatabase(loadedDatabase);

            // 资源都已就绪，现在安全地切换到菜单状态
            ChangeState<ProcedureMenu>(m_FSM);
        }
        else
        {
            Debug.LogError("无法加载音效数据库，游戏无法继续。");
        }
    }

    public static ProcedureLaunch Create()
    {
        ProcedureLaunch procedure = new ProcedureLaunch();
        return procedure;
    }
}