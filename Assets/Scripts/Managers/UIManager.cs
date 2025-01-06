using UnityEngine;
public class UIManager : MonoSingleton<UIManager>
{

    public GameObject PauseMenuUI;
    public bool IsTryOpenPauseMenu => Input.GetKeyDown(KeyCode.Escape);
    private bool isOpenPauseMenu = false;


    private void Update() {
            
        if(IsTryOpenPauseMenu)
        {
            SwitchPauseMenu();
        }
    }

    public void SwitchPauseMenu()
    {
        isOpenPauseMenu = !isOpenPauseMenu;
        PauseMenuUI.SetActive(isOpenPauseMenu);
        if(isOpenPauseMenu)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    
}