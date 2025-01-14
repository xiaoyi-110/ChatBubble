
using UnityEngine;

public class LevelForm : UIForm
{
    private ProcedureLevel m_ProcedureLevel;
    [SerializeField] private GameObject PauseMenu;
    private bool m_IsTryOpenPauseMenu => Input.GetKeyDown(KeyCode.Escape);
    private bool m_IsPauseMenuActive => PauseMenu.activeSelf;

    private void Update()
    {
        if (m_IsTryOpenPauseMenu)
        {
            SwitchPauseMenu();
        }
    }


    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);

        if (userData == null)
        {
            Debug.LogError("ProcedureLevel is null");
            return;
        }

        m_ProcedureLevel = userData as ProcedureLevel;
    }

    protected override void OnClose(object userData)
    {
        m_ProcedureLevel = null;
        base.OnClose(userData);
    }

    private void SwitchPauseMenu()
    {
        PauseMenu.SetActive(!m_IsPauseMenuActive);

        LevelManager.Instance.IsPause = m_IsPauseMenuActive;
        Time.timeScale = m_IsPauseMenuActive ? 0 : 1;
    }

    public void OnClickBackToGameButton()
    {
        SwitchPauseMenu();
    }

    public void OnClickBackToMenuButton()
    {
        Time.timeScale = 1;
        m_ProcedureLevel.BackToMenu();
    }
}