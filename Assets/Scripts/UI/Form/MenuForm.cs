using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuForm : UIForm
{
   


    public void OnStartButtonClick()
    {
        UIManager.Instance.ShowUIForm("LevelForm");
        LevelManager.Instance.StartGame();
    }

    public void OnTeamIntroButtonClick()
    {
        UIManager.Instance.ShowUIForm("TeamIntroForm");
    }
    

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
