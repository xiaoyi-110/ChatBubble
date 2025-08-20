using UnityEngine;

public class ResultPhase : ILevelPhase
{
    private LevelManager m_manager;
    private float m_phaseTimer;
    private LevelResultData m_resultData;
    public ResultPhase(LevelManager manager, LevelResultData resultData)
    {
        m_manager = manager;
        m_resultData = resultData;
    }


    public void EnterPhase()
    {
        m_phaseTimer = 0f;
        LevelManager.Instance.ShowLevelResult(m_resultData);
        Debug.Log("Entering ResultPhase");
    }

    public void UpdatePhase(float deltaTime)
    {
        m_phaseTimer += deltaTime;
        if (m_phaseTimer >= m_manager.ResultPhaseDuration)
        {
            ExitPhase();
        }
    }

    public void ExitPhase()
    {
        m_phaseTimer = 0f;
        LevelManager.Instance.HideLevelResult();
        Debug.Log("Exiting ResultPhase");
        m_manager.SetState(new DefensePhase(m_manager));
    }
}
