using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]private Button restartButton;
    [SerializeField]private Button backButton;

    private void Awake() {
        restartButton = transform.Find("RestartButton").GetComponent<Button>();
        backButton = transform.Find("BackButton").GetComponent<Button>();

        restartButton.onClick.AddListener(OnRestartButtonClicked);
        backButton.onClick.AddListener(OnBackButtonClicked);
    }


    public void OnRestartButtonClicked()
    {
        GameManager.Instance.ResetLevel();
        UIManager.Instance.SwitchPauseMenu();
    }

    public void OnBackButtonClicked()
    {
        UIManager.Instance.SwitchPauseMenu();
    }
}
