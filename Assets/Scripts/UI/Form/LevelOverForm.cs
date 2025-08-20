

public class LevelOverForm : UIForm
{
    public void OnRestartLevelButtonClick()
    {
        UIManager.Instance.ShowUIForm("LevelForm");
        LevelManager.Instance.RestartLevel();
    }
    
}

