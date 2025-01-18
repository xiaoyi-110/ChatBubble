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

        
        Init(3);
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
        
    }
    public void OnPlayerHpChange(object sender, EventArgs e)
    {
        Debug.Log(sender);
        OnPlayerHPChangeEventArgs args = e as OnPlayerHPChangeEventArgs;
        int hp = args.HP;
        Debug.Log(hp);
        if(HP != hp)
        {
            HP = hp;
            Debug.Log("HP Change:" + HP);

            for(int i = 1;i<=MaxHP;i++)
            {
                if(Bloods[i-1] == null)continue;
                
                Bloods[i-1].SetActive(i<=HP);
            }

        }
    }


}
