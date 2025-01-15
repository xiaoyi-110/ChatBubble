using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class TeamIntroForm : UIForm
{

    private ProcedureTeamIntro m_ProcedureTeamIntro;
    private bool m_IsTryExit => Input.anyKey;


    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);

        m_ProcedureTeamIntro = (ProcedureTeamIntro)userData;
    }

    private void Start() {
        
          
    }

    private void Update() {
        if(m_IsTryExit) {
            m_ProcedureTeamIntro.StartState(ProcedureTeamIntro.ProcedureTeamIntroState.Exit);
        }
    }



}
