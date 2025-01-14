using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuForm : UIForm
{
    private ProcedureMenu m_ProcedureMenu;

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);

        if(userData == null)
        {
            Debug.LogError("ProcedureMenu is null");
            return;
        }

        m_ProcedureMenu = userData as ProcedureMenu;
    }

    protected override void OnClose(object userData)
    {
        m_ProcedureMenu = null;
        base.OnClose(userData);
    }


    public void OnStartButtonClick()
    {
        m_ProcedureMenu.StartGame();
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
