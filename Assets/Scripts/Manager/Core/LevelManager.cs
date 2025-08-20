using UnityEngine;
public class LevelManager : MonoSingleton<LevelManager>
{
    private bool m_IsTryPauseLevel => Input.GetKeyDown(KeyCode.Escape);
    public float GlobalTimer { get; private set; } = 0f;
    private ILevelPhase m_currentPhase;
    public Player Player => EntityRegistry.Get<Player>(EntityType.Player);
    public Boss Boss => EntityRegistry.Get<Boss>(EntityType.Boss);

    public bool IsGameStarted;

    public int CurrentLevel { get; private set; } = 1;
    private int m_TotalRounds = 6;  


    public float DefensePhaseDuration = 24f;
    public float AttackPhaseDuration = 7f;
    public float ResultPhaseDuration = 3f;

    protected override void Awake()
    {
        base.Awake();
        Time.timeScale = 0; // 初始时暂停游戏
        m_currentPhase = null;
        IsGameStarted = false;
    }
    public void SetState(ILevelPhase newPhase)
    {
        m_currentPhase = newPhase;
        m_currentPhase?.EnterPhase();
    }


    private void Start()
    {
        //AudioManager.Instance.Play("BGM_Main");
        InitializeLevel();
        SetState(new DefensePhase(this));
    }

    public void StartGame()
    {
        InitializeLevel();
        IsGameStarted = true;
        Time.timeScale = 1;
        SetState(new DefensePhase(this));
    }

    public void QuitGame()
    {
        BulletManager.Instance.ClearBullets();
        ArrowManager.Instance.ClearWave();
        InitializeLevel();
        IsGameStarted = false;
        Time.timeScale = 0;
    }

    public void EndLevel()
    {
        UIManager.Instance.ShowUIForm("LevelOverForm");
        Time.timeScale = 0;
    }

    public void InitializeLevel()
    {
        GlobalTimer = 0f;
        Player.Init();
        Boss.Init();
        CurrentLevel = 1;
    }
    private void Update()
    {
        if (!IsGameStarted) return;

        if (m_IsTryPauseLevel)
        {
            PauseLevel();
            return;
        }

        GlobalTimer += Time.deltaTime;

        m_currentPhase?.UpdatePhase(Time.deltaTime);
    }

    public void UpdateLevel(int index)
    {
        switch (index)
        {
            case 0:
                Boss.ChangeHP(-2);
                Player.ChangeHP(1);
                break;
            case 1:
                Player.ChangeHP(1);
                break;
            case 2:
                break;
            case 3:
                Player.ChangeHP(-1);
                break;
            default:
                Debug.LogWarning("Invalid level value.");
                break;
        }
    }
    public void ShowLevelResult(LevelResultData data)
    {
        EventManager.Instance.TriggerEvent(LevelResultEventArgs.EventId, this, new LevelResultEventArgs(data, true));
    }

    public void HideLevelResult()
    {
        LevelResultData resultData = ArrowPhaseController.Instance.LevelResultData;
        EventManager.Instance.TriggerEvent(LevelResultEventArgs.EventId, this, new LevelResultEventArgs(new LevelResultData(), false));

        if (CurrentLevel <= m_TotalRounds)
        {
            CurrentLevel++;
            UpdateLevel(resultData.RankIndex);
            Debug.Log("Current Level: " + CurrentLevel);
        }

        if (CurrentLevel > m_TotalRounds && Boss.CurrentHP > 0) { EndLevel(); }
    }

    public void PauseLevel()
    {
        UIManager.Instance.ShowUIForm("PauseMenuForm");
        Time.timeScale = 0;
    }

    public void ResumeLevel()
    {
        Time.timeScale = 1;
    }
    public void LevelOver()
    {
        UIManager.Instance.ShowUIForm("LevelOverForm");
        Time.timeScale = 0;
    }

    public void RestartLevel()
    {     
        BulletManager.Instance.ClearBullets();
        ArrowManager.Instance.ClearWave();
        StartGame();        
    }

    public void LevelSuccess()
    {
        AudioManager.Instance.Stop("BGM_Main");
        StartCoroutine(AudioManager.Instance.FadeIn("BGM_Victory", 2f));
        UIManager.Instance.ShowUIForm("LevelSuccessForm");
        IsGameStarted = false;
    }

}
