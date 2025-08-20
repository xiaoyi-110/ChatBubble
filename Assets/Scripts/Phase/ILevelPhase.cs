public interface ILevelPhase
{
    void EnterPhase();
    void UpdatePhase(float deltaTime);
    void ExitPhase();
}
