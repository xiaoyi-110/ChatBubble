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
            // �����غõ����ݿ⴫�ݸ� AudioManager
            AudioManager.Instance.SetSoundDatabase(loadedDatabase);

            // ��Դ���Ѿ��������ڰ�ȫ���л����˵�״̬
            ChangeState<ProcedureMenu>(m_FSM);
        }
        else
        {
            Debug.LogError("�޷�������Ч���ݿ⣬��Ϸ�޷�������");
        }
    }

    public static ProcedureLaunch Create()
    {
        ProcedureLaunch procedure = new ProcedureLaunch();
        return procedure;
    }
}