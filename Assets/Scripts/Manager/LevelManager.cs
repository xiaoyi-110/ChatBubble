

using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{

    public List<GameObject> Spawns;
    private LevelState m_LevelState;
    public LevelData m_LevelData;
    public BulletPrefabData m_BulletPrefabData;
    public Transform BulletRoot;
    private Dictionary<int, GameObject> m_BulletPrefabDict;
    private float m_LevelTimer;
    private int m_BulletIndex;
    
    
    
    public enum LevelState{
        PlayerAttack,
        PlayerAovid
    }

    private void Start() {

        m_LevelState = LevelState.PlayerAovid;

        m_LevelData.bullets.Sort();

        // 读取子弹Prefab数据
        m_BulletPrefabDict = new Dictionary<int, GameObject>();
        for(int i = 0; i < m_BulletPrefabData.bulletPrefabs.Count; i++)
        {
            m_BulletPrefabDict.Add(i, m_BulletPrefabData.bulletPrefabs[i]);
        }

    }

    public void InitLevel()
    {
        m_LevelTimer = 0f;
        m_BulletIndex = 0;
    }

    private void Update() {

        m_LevelTimer += Time.deltaTime;

        switch(m_LevelState){
            case LevelState.PlayerAttack:
                //TODO
                break;
            case LevelState.PlayerAovid:
                if(m_BulletIndex < m_LevelData.bullets.Count && m_LevelTimer >= m_LevelData.bullets[m_BulletIndex].SpawnTime)
                {
                    SpawnBullet(m_LevelData.bullets[m_BulletIndex]);
                }
                break;
            default:
                break;  
        }

        
    }

    private void SpawnBullet(BulletData bulletData)
    {
        GameObject bullet = Instantiate(m_BulletPrefabDict[bulletData.Type], BulletRoot.transform);
        bullet.GetComponent<Bullet>().Init(bulletData);
        bullet.GetComponent<Bubble>()?.Init(bulletData);


        m_BulletIndex++;
        // 循环发射弹幕
        if(m_BulletIndex >= m_LevelData.bullets.Count)
        {
            m_BulletIndex = 0;
            m_LevelTimer = 0;
        }
       
        bullet.transform.position = Spawns[bulletData.SpawnIndex].transform.position;
    }
}