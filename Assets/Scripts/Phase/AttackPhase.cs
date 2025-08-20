using UnityEngine;

public class AttackPhase : ILevelPhase
{
    private LevelManager m_manager;
    private float m_phaseTimer;
    public AttackPhase(LevelManager manager)
    {
        m_manager = manager;
    }

    public void EnterPhase()
    {
        m_phaseTimer = 0f;
        ArrowPhaseController.Instance.ResetLevel(m_manager.CurrentLevel);
        Debug.Log("Entering AttackPhase");
    }

    public void UpdatePhase(float deltaTime)
    {
        m_phaseTimer += deltaTime;
        if (m_phaseTimer >= m_manager.AttackPhaseDuration)
        {
            ExitPhase();
        }
    }

    public void ExitPhase()
    {
        m_phaseTimer = 0f;
        ArrowPhaseController.Instance.ClearArrows();
        Debug.Log("Exiting AttackPhase");
        m_manager.SetState(new ResultPhase(m_manager, ArrowPhaseController.Instance.LevelResultData));
    }
}
