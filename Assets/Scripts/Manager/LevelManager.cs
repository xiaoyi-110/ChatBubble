

using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    
    private bool m_IsTryPauseLevel => Input.GetKeyDown(KeyCode.Escape);
    public List<GameObject> Spawns;
    private LevelState m_LevelState;
    public LevelData_SO m_LevelData;
    public Transform BulletRoot; 
    public Player m_Player;
    public Boss m_Boss;
    private float m_LevelTimer;
    private int m_BulletIndex;
    
    public bool m_IsStartGame;
    
    public enum LevelState{
        PlayerAttack,
        PlayerAovid
    }

    private void Start() {

        m_LevelState = LevelState.PlayerAovid;
        m_LevelData.Sheet1.Sort();
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

    public void RestartLevel()
    {
        InitLevel();
        StartGame();
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
            case LevelState.PlayerAttack:
                //TODO
                break;
            case LevelState.PlayerAovid:
                if(m_BulletIndex < m_LevelData.Sheet1.Count && m_LevelTimer >= m_LevelData.Sheet1[m_BulletIndex].Time)
                {
                    SpawnBullet(m_LevelData.Sheet1[m_BulletIndex]);
                }
                break;
            default:
                break;  
        }

        
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
       
        
    }

    public void ReStartLevel()
    {
        InitLevel();
        ClearBullet();
        StartGame();
    }

    public void LevelSuccess()
    {
        
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
