

public class LevelSuccessForm : UIForm
{
    public void OnBackToMenuButtonClick()
    {
        UIManager.Instance.ShowUIForm("MenuForm");
        LevelManager.Instance.QuitGame();
    }
    
}

