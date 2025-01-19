using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public void Awake()
    {

    }


    public void Start()
    {
        UIManager.Instance.ShowUIForm("MenuForm");
    }
    /*public static GameManager S;
    private void Awake()
    {
        S = this;
    }

    int currentLevel = 0; // Start from level 0 to have levels 1-6
    public static bool isDancing;

    private void Start()
    {
        NextWave();
    }

    void NextWave()
    {
        if (currentLevel < 6) // Only proceed if there are more levels to play
        {
            ArrowManager.S.CreateWave(currentLevel);
            isDancing = true;
        }
        else
        {
            // Game over or win logic here
            Debug.Log("All levels completed!");
        }
    }

    public void FinishWave()
    {
        currentLevel++;
        isDancing = false;
        Invoke("NextWave", 3);
        ArrowManager.S.ClearWave();
    }

    public void FailWave()
    {
        isDancing = false;
        Invoke("NextWave", 3);
        ArrowManager.S.ClearWave();
    }*/
}
