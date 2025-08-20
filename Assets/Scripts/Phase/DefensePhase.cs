using UnityEngine;

public class DefensePhase : ILevelPhase
{
    private LevelManager m_manager;
    private float m_phaseTimer;
    public DefensePhase(LevelManager manager)
    {
        m_manager = manager;
    }

    public void EnterPhase()
    {
        m_phaseTimer = 0f;
        BulletManager.Instance.ResetLevel(m_manager.CurrentLevel);
        Debug.Log("Entering Defense Phase");
    }

    public void UpdatePhase(float deltaTime)
    {
        m_phaseTimer += deltaTime;
        BulletManager.Instance.UpdateSpawn(m_phaseTimer);
        if (m_phaseTimer >=m_manager.DefensePhaseDuration)
        {
            ExitPhase();
        }
    }

    public void ExitPhase()
    {
        m_phaseTimer = 0f;
        BulletManager.Instance.ClearBullets();
        Debug.Log("Exiting Defense Phase");
        m_manager.SetState(new AttackPhase(m_manager));
    }
}
