using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoSingleton<LevelManager>
{
    
    private bool m_IsTryPauseLevel => Input.GetKeyDown(KeyCode.Escape);
    public List<GameObject> Spawns;
    public LevelState m_LevelState;
    public LevelData_SO m_LevelData;
    public Transform BulletRoot; 
    public Player m_Player;
    public Boss m_Boss;
    private float m_LevelTimer;
    private int m_BulletIndex;
    private float TotalTime;
    
    public bool m_IsStartGame;

    public static bool isDancing;
    private int currentLevel = 1;
    private int correctInputs;
    private int wrongInputs;
    private int totalRounds = 6;
    public int rankIndex=4;

    public GameObject resultPanel;  // 结果面板（用于显示等级）
    public TextMeshProUGUI resultText;  // 结果面板上的文本（显示Rank和文案）

    public float defenseDuration = 24f; // 防御阶段持续时间
    public float attackDuration = 7f;  // 攻击阶段持续时间
    public float resultDuration = 3f;  // 结果展示时间

    public static LevelManager S;
    [SerializeField] private AudioSource victorySound;
    private void Awake()
    {
        S = this;
    }
    // 24个文案，6关，每关4个等级（S、A、B、C）
    public string[,] levelRankText = new string[6, 4]
    {
        { "关卡1 S：辛苦你一直以来还能硬着头\n" +
            "皮看下去。",
            "关卡1 A：我一定配合调整！毕竟咱们\n" +
            "是一个团队，对吧？",
            "关卡1 B：急急急，你先别急", "关卡1 C：狗在叫！" },
        { "关卡2 S：你骂得对，是我没考虑到实\n" +
            "现的复杂性。", "关卡2 A：下次我把需求整理得更清楚\n" +
            "，提前拉你们讨论，绝不再让你受这\n" +
            "种折磨。",
            "关卡2 B：你看，又急", "关卡2 C：你说得对，但是原神...、" },
        { "关卡3 S：以后每个需求发布前，我一\n" +
            "定先和你们聊一聊，确认可行性。这\n" +
            "次我真是没做好，给你们带来麻烦了。",
            "关卡3 A：对不起，这次真是我疏忽了\n" +
            "，需求改得太频繁了。完全理解你们\n" +
            "有多崩溃，这样真是让人难以跟得上。",
            "关卡3 B：改个策划案有什么大不了的\n" +
            "，反正代码也天天改。",
            "关卡3 C：菜就多练。需求改三次不算\n" +
            "多吧？你们程序员不都是要加班的嘛。" },
        { "关卡4 S：完全理解，真的感谢你一直\n" +
            "在努力。", "关卡4 A：以后我会提前和你们沟通。\n" +
            "确保需求明确且可行。",
            "关卡4 B：嘻嘻。", "关卡4 C：包的啊！" },
        { "关卡5 S：我完全理解你的感受，确实\n" +
            "我在写策划案时没考虑到实现成本，\n" +
            "我考虑不周，给你们带来了很多额外\n" +
            "压力，非常抱歉。",
            "关卡5 A：我知道你们面临的技术难度\n" +
            "比我想象的要大，我以后一定会在写\n" +
            "策划案时更加关注可行性和实际成本。",
            "关卡5 B：包的。", "关卡5 C：小题大做，菜就多练。" },
        { "关卡6 S：对不起！！Orz", "关卡6 A：对不起！！Orz", "关卡6 B：对不起！！Orz", "关卡6 C：对不起！！Orz" }
    };

    public enum LevelState
    {
        PlayerAttack, // 玩家攻击阶段
        PlayerAvoid,  // 玩家躲避阶段
        ShowResult    // 显示结果阶段
    }
    private void Start() {

        m_LevelState = LevelState.PlayerAvoid;
        resultPanel.SetActive(false);
        InitLevel();
        m_IsStartGame = false;
    }

    public void StartGame()
    {
        m_IsStartGame = true;
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        m_IsStartGame = false;
    }


    public void LevelOver()
    {
        UIManager.Instance.ShowUIForm("LevelOverForm");
        Time.timeScale = 0;
    }

    public void InitLevel()
    {
        m_LevelTimer = 0f;
        m_BulletIndex = 0;
        m_Player = GameObject.Find("Player").GetComponent<Player>();
        m_Boss = GameObject.Find("Boss").GetComponent<Boss>();
        if(m_Player == null)
        {
            Debug.LogError("Player is null");
        }
        m_Player.Init();
        m_Boss.Init();
        
    }

    private void Update() {
        if(!m_IsStartGame) return;

        if(m_IsTryPauseLevel)
        {
            PauseLevel();
            return;
        }

        m_LevelTimer += Time.deltaTime;

        switch(m_LevelState){
            case LevelState.PlayerAvoid:
                HandleDefensePhase();
                break;

            case LevelState.PlayerAttack:
                HandleAttackPhase();
                break;

            case LevelState.ShowResult:
                HandleResultPhase();
                break;
            default:
                break;  
        }       
    }
    //防御阶段发射弹幕
    void HandleDefensePhase()
    {
        int levelStartIndex = GetLevelStartIndex();
        int bulletsInCurrentLevel = GetBulletsForCurrentLevel(currentLevel);

        if (m_BulletIndex < bulletsInCurrentLevel)
        {
            TotalTime = 0;
            for (int i = 1; i < currentLevel; i++)
            {
                TotalTime += 34f;
            }
            TotalTime += m_LevelTimer;
            if (TotalTime >= m_LevelData.Sheet1[levelStartIndex + m_BulletIndex].Time)
            {
                SpawnBullet(m_LevelData.Sheet1[levelStartIndex + m_BulletIndex]);
            }
        }

        if (m_LevelTimer >= defenseDuration)
        {
            m_LevelState = LevelState.PlayerAttack;
            m_LevelTimer = 0f;
            StartAttackPhase();
        }
    }

    // 获取当前关卡应该发射的子弹数量
    int GetBulletsForCurrentLevel(int currentLevel)
    {
        switch (currentLevel)
        {
            case 1: return 20; //0-19
            case 2: return 22; //20-41
            case 3: return 22; //42-63
            case 4:return 22;//64-85
            case 5:return 22;//86-107
            case 6:return 22;//108-129                
            default: Debug.LogError("Wrong Level!");
                return 0;
        }
    }
    // 计算当前关卡在 Sheet1 中的起始索引
    int GetLevelStartIndex()
    {
        // 计算当前关卡在 Sheet1 中的起始索引
        int startIndex = 0;
        for (int i = 1; i < currentLevel; i++)
        {
            startIndex += GetBulletsForCurrentLevel(i); // 累加前几关的子弹数量
        }
        return startIndex;
    }
    //攻击阶段Q炫舞
    void HandleAttackPhase()
    {
        // 攻击阶段结束
        if (m_LevelTimer >= attackDuration)
        {
            m_LevelState = LevelState.ShowResult;
            m_LevelTimer = 0f;
            FinishWave(); 
        }
    }


    //结果显示阶段
    void HandleResultPhase()
    {
        resultPanel.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(resultPanel.GetComponent<RectTransform>());
        // 结果展示结束
        if (m_LevelTimer >= resultDuration)
        {
            resultPanel.SetActive(false);
            if (currentLevel <= totalRounds)
            {
                currentLevel++;
                m_LevelState = LevelState.PlayerAvoid;
                m_LevelTimer = 0f;
                m_BulletIndex = 0;
            }
            if (currentLevel >totalRounds && m_Boss.CurrentHP > 0)
            {
                LevelOver();
            }
        }
    }

    public void UpdateLevel(int index)
    {
        switch (index)
        {
            case 0:
                m_Boss.ChangeHP(-2);
                m_Player.ChangeHP(1);
                break;
            case 1:
                m_Player.ChangeHP(1);
                break;
            case 2:
                break;
            case 3:
                m_Player.ChangeHP(-1);
                break;
            default:
                Debug.LogWarning("Invalid level value.");
                break;
        }
    }

    // 清空弹幕并生成箭头
    void StartAttackPhase()
    {
        ClearBullet();
        if (ArrowManager.S != null)
        {
            if (currentLevel == 1 || currentLevel == 2) { ArrowManager.S.CreateWave(9); }
            else if (currentLevel == 3 || currentLevel == 4) { ArrowManager.S.CreateWave(10); }
            else if (currentLevel == 5 || currentLevel == 6) { ArrowManager.S.CreateWave(11); }

        }
        else
        {
            Debug.LogError("ArrowManager instance is missing!");
        }
        isDancing = true;
        correctInputs = 0;
        wrongInputs = 0;
    }

    //处理每关结束
    public void FinishWave()
    {
        isDancing = false;
        ArrowManager.S.ClearWave();
        ShowResult();
        UpdateLevel(rankIndex);
        rankIndex = 4;
    }

    // 记录正确和错误输入的数量
    public void RecordInput(bool isCorrect)
    {
        if (isCorrect)
        {
            correctInputs++;
        }
        else
        {
            wrongInputs++;
        }
    }
    // 显示结果（可以弹出一个UI面板显示等级）
    void ShowResult()
    {
        resultPanel.SetActive(true);
        int levelIndex = Mathf.Clamp(currentLevel - 1, 0, 5); // 当前关卡索引
        float accuracy = correctInputs / 10f; // 假设每关10个箭头

        // 评定等级
        string rank = accuracy >= 1f ? "S" :
                     accuracy >= 0.8f ? "A" :
                     accuracy >= 0.6f ? "B" : "C";

        // 获取对应文案
        rankIndex = rank == "S" ? 0 : rank == "A" ? 1 : rank == "B" ? 2 : 3;
        resultText.text = levelRankText[levelIndex, rankIndex];
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

    private void SpawnBullet(BulletData bulletData)
    {
        GameObject bullet;
        if(bulletData.IsAttackable == 1)
        {
            bullet = ObjectPool.Instance.GetObject(bulletData.PrefabName+"2");
        }
        else
        {
            bullet = ObjectPool.Instance.GetObject(bulletData.PrefabName);
        }
         
        bullet.transform.SetParent(BulletRoot);
        bullet.GetComponent<Bullet>().Init(bulletData);
        bullet.GetComponent<Bubble>()?.Init(bulletData);
        bullet.GetComponent<Emoji>()?.Init(bulletData);
        bullet.transform.position = Spawns[bulletData.SpawnIndex].transform.position;

        m_BulletIndex++;
        // 循环发射弹幕
        if(m_BulletIndex >= m_LevelData.Sheet1.Count)
        {
            m_BulletIndex = 0;
            m_LevelTimer = 0;
        }
        Debug.Log($"生成子弹：关卡{currentLevel} 索引{GetLevelStartIndex() + m_BulletIndex}");
    }

    public void ReStartLevel()
    {
        InitLevel();
        ClearBullet();
        StartGame();
        currentLevel = 1;
    }

    public void LevelSuccess()
    {
        AudioManager.Instance.StopBGM();
        victorySound.Play();
        UIManager.Instance.ShowUIForm("LevelSuccessForm");
        m_IsStartGame = false;
        
    }

    public void ClearBullet()
    {
        // 获取BulletRoot下所有的子GameObject

        foreach (Transform child in BulletRoot.transform)
        {
            ObjectPool.Instance.RecycleObject(child.gameObject);
        }
    }
       
}
