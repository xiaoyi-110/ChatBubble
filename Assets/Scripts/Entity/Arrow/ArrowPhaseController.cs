using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPhaseController : MonoSingleton<ArrowPhaseController>
{
    public static bool IsDancing;
    private int m_CorrectInputCount;
    private int m_WrongInputCount;
    private int m_CurrentLevel;
    private ArrowManager m_ArrowManager;
    public LevelResultData LevelResultData;

    private void Start()
    {
        m_ArrowManager = ArrowManager.Instance;
        if (m_ArrowManager == null)
        {
            Debug.LogError("ArrowManager instance is missing!");
        }
        ResetStats();
    }
    public void ResetLevel(int currentLevel)
    {
        m_CurrentLevel = currentLevel;
        ResetStats();

        if (m_ArrowManager != null)
        {
            int waveSize = m_CurrentLevel <= 2 ? 9 :
                           m_CurrentLevel <= 4 ? 10 : 11;
            m_ArrowManager.CreateWave(waveSize);
        }
        else
        {
            Debug.LogError("ArrowManager instance is missing!");
        }
        IsDancing = true;
    }

    // 处理每关结束  
    public void ClearArrows()
    {
        IsDancing = false;
        m_ArrowManager.ClearWave();
        LevelResultData = new LevelResultData(m_CurrentLevel, m_CorrectInputCount, m_WrongInputCount);
    }

    // 记录正确和错误输入的数量  
    public void RecordInput(bool isCorrect)
    {
        if (isCorrect)
        {
            m_CorrectInputCount++;
        }
        else
        {
            m_WrongInputCount++;
        }
    }

    private void ResetStats()
    {
        m_CorrectInputCount = 0;
        m_WrongInputCount = 0;
        IsDancing = false;
    }
}
