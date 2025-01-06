using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]private GameObject cinemachineCamera;
    public string currentLevel;

    private void Awake() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start() {     
        currentLevel = "Level_0";
        SwitchCameraBoder();
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name == currentLevel)
        {
            SwitchCameraBoder();
        }
    }

    /// <summary>
    /// 加载关卡
    /// </summary>
    /// <param name="levelName">关卡名</param>
    public void LoadLevel(string levelName) {
        currentLevel = levelName;
        SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
        InitLevel();
    }

    //TODO: 初始化关卡
    private void InitLevel()
    {
        throw new NotImplementedException();
    }

    public void ResetLevel() {
        SceneManager.UnloadSceneAsync(currentLevel);
        LoadLevel(currentLevel);
    }

    private void SwitchCameraBoder()
    {
        CinemachineConfiner2D confiner2D = cinemachineCamera.GetComponent<CinemachineConfiner2D>();
        confiner2D.m_BoundingShape2D = GameObject.Find("CameraBorder").GetComponent<PolygonCollider2D>();
        confiner2D.InvalidateCache();
    }

   
}

