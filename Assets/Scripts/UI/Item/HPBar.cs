using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public string BarName;
    public int MaxHP;
    public int HP;
    public GameObject HPLayout;
    public GameObject BloodPrefab;
    public List<GameObject> Bloods;

    private void Awake() {
        
    }

    void Start()
    {
        if(HPLayout == null)
        {
            Debug.LogError("HPLayout is null");
        }
        if(Bloods == null)
        {
            Debug.LogError("BloodPrefab is null");
        }

        EventManager.Instance.RegisterEvent(OnHPChangeEventArgs.EventId, OnHpChange);
        
        if(name == "PlayerHPBar")
        {
            Init(LevelManager.Instance.m_Player.MaxHP);
        }
        else 
        {
            Init(LevelManager.Instance.m_Boss.MaxHP);
        }
        
    }

    public void Init(int hp)
    {
        MaxHP = HP = hp;
        Bloods.Clear();
        for(int i=1;i<=MaxHP;i++)
        {
            GameObject blood = Instantiate(BloodPrefab, HPLayout.transform);
            blood.name = "Blood" + i;
            Bloods.Add(blood);
            
        } 
        show();
    }

    public void show()
    {
        int hp ;

        if(name == "PlayerHPBar")
        {
            hp = LevelManager.Instance.m_Player.CurrentHP;
        }
        else
        {
            hp = LevelManager.Instance.m_Boss.CurrentHP;
            Debug.Log(hp);
        }

        for(int i=1;i<=MaxHP;i++)
        {
            Bloods[i-1].SetActive(i<=hp);
        }
    }
    
    public void OnHpChange(object sender, EventArgs e)
    {
        OnHPChangeEventArgs args = e as OnHPChangeEventArgs;
        if(args.target != BarName)return;
        int hp = args.HP;
        

        Debug.Log(args.target + " HP Change " + hp);

        if(HP != hp)
        {
            HP = hp;

            for(int i = 1;i<=MaxHP;i++)
            {
                if(Bloods[i-1] == null)continue;
                
                Bloods[i-1].SetActive(i<=HP);
            }

        }
    }


}
