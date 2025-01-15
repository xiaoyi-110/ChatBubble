
using UnityEngine;

public class LevelForm : UIForm
{
    private ProcedureLevel m_ProcedureLevel;
    [SerializeField] private GameObject PauseMenuPanel;
    [SerializeField] private GameObject ControlIntroPanel;
    private bool m_IsTryOpenPauseMenu => Input.GetKeyDown(KeyCode.Escape);
    private bool m_IsTryCloseControlIntro =>  ControlIntroPanel.activeSelf && Input.anyKey;
    private bool m_IsPauseMenuActive => PauseMenuPanel.activeSelf;

    private void Update()
    {
        if (m_IsTryOpenPauseMenu)
        {
            SwitchPauseMenu();
        }
        if(m_IsTryCloseControlIntro)
        {
            CloseControlIntroPanel();
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
        PauseMenuPanel.SetActive(!m_IsPauseMenuActive);

        LevelManager.Instance.IsPause = m_IsPauseMenuActive;
    }

    private void CloseControlIntroPanel()
    {
        ControlIntroPanel.SetActive(false);
        LevelManager.Instance.IsPause = false;
    }

    public void OnBackToGameButtonClick()
    {
        SwitchPauseMenu();
    }

    public void OnBackToMenuButtonClick()
    {
        m_ProcedureLevel.BackToMenu();
    }

    public void OnOpenControlIntroButtonClick()
    {
        PauseMenuPanel.SetActive(false);
        ControlIntroPanel.SetActive(true);
    }
}