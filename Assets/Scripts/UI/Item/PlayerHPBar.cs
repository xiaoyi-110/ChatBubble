using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPBar : MonoBehaviour
{
    public int MaxHP;
    public int HP;
    public GameObject HPLayout;
    public GameObject BloodPrefab;
    public List<GameObject> Bloods;

    private void Awake() {
        Init(LevelManager.Instance.m_Player.MaxHP);
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

        EventManager.Instance.RegisterEvent(OnPlayerHPChangeEventArgs.EventId, OnPlayerHpChange);

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
        int hp = LevelManager.Instance.m_Player.CurrentHP;
        for(int i=1;i<=MaxHP;i++)
        {
            Bloods[i-1].SetActive(i<=hp);
        }
    }
    
    public void OnPlayerHpChange(object sender, EventArgs e)
    {
        OnPlayerHPChangeEventArgs args = e as OnPlayerHPChangeEventArgs;
        int hp = args.HP;
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
