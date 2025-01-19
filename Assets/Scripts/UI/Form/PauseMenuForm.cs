using UnityEngine;

public class PauseMenuForm : UIForm
{

    public void OnBackToMenuButtonClick()
    {
        LevelManager.Instance.QuitGame();
        UIManager.Instance.ShowUIForm("MenuForm");
    }

    public void OnBackToGameButtonClick()
    {
        UIManager.Instance.ShowUIForm("LevelForm");
        LevelManager.Instance.ResumeLevel();
    }
}