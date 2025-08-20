using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoSingleton<BulletManager>
{
    public List<GameObject> SpawnPoints;
    public Transform BulletContainer;
    public LevelData_SO LevelData;

    private int m_BulletIndex;
    private float m_TotalTime;
    private int m_CurrentLevel;

    public void ResetLevel(int currentLevel)
    {
        m_CurrentLevel = currentLevel;
        m_BulletIndex = 0;
    }

    public void UpdateSpawn(float phaseTimer)
    {
        int levelStartIndex = GetLevelStartIndex();
        int bulletsInCurrentLevel = GetBulletsForCurrentLevel(m_CurrentLevel);

        if (m_BulletIndex < bulletsInCurrentLevel)
        {
            m_TotalTime = LevelManager.Instance.GlobalTimer;
            if (m_TotalTime >= LevelData.Sheet1[levelStartIndex + m_BulletIndex].Time)
            {
                SpawnBullets(LevelData.Sheet1[levelStartIndex + m_BulletIndex]);
            }
        }
    }

    public void SpawnBullets(BulletData bulletData)
    {
        GameObject bullet;
        if (bulletData.IsAttackable == 1)
        {
            bullet = ObjectPool.Instance.GetObject(bulletData.PrefabName + "2");
        }
        else
        {
            bullet = ObjectPool.Instance.GetObject(bulletData.PrefabName);
        }

        bullet.transform.SetParent(BulletContainer);
        bullet.GetComponent<Bullet>().Init(bulletData);
        bullet.GetComponent<Bubble>()?.Init(bulletData);
        bullet.GetComponent<Emoji>()?.Init(bulletData);
        bullet.transform.position = SpawnPoints[bulletData.SpawnIndex].transform.position;

        m_BulletIndex++;
        if (m_BulletIndex >= LevelData.Sheet1.Count)
        {
            m_BulletIndex = 0;
        }
        Debug.Log($"生成子弹：关卡{m_CurrentLevel} 索引{GetLevelStartIndex() + m_BulletIndex}");
    }
   

    private int GetBulletsForCurrentLevel(int currentLevel)
    {
        switch (currentLevel)
        {
            case 1: return 20;
            case 2: return 22;
            case 3: return 22;
            case 4: return 22;
            case 5: return 22;
            case 6: return 22;
            default:
                Debug.LogWarning("Wrong Level!");
                return 0;
        }
    }

    // 计算当前关卡在 Sheet1 中的起始索引  
    private int GetLevelStartIndex()
    {
        int startIndex = 0;
        for (int i = 1; i < m_CurrentLevel; i++)
        {
            startIndex += GetBulletsForCurrentLevel(i);
        }
        return startIndex;
    }

    public void ClearBullets()
    {
        foreach (Transform child in BulletContainer.transform)
        {
            ObjectPool.Instance.RecycleObject(child.gameObject);
        }
    }
}
