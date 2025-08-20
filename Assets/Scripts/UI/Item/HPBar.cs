using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public EntityType BarName;
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

        int initialHP = BarName == EntityType.Player
                        ? LevelManager.Instance.Player.MaxHP
                        : LevelManager.Instance.Boss.MaxHP;
        Init(initialHP);
    }

    public void Init(int hp)
    {
        MaxHP = HP = hp;
        Bloods.Clear();

        for (int i = 0; i < MaxHP; i++)
        {
            GameObject blood = Instantiate(BloodPrefab, HPLayout.transform);
            blood.name = "Blood" + (i + 1);
            Bloods.Add(blood);
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        int currentHP = BarName == EntityType.Player
                        ? LevelManager.Instance.Player.CurrentHP
                        : LevelManager.Instance.Boss.CurrentHP;

        for (int i = 0; i < MaxHP; i++)
        {
            if (Bloods[i] != null)
                Bloods[i].SetActive(i < currentHP);
        }
    }

    public void OnHpChange(object sender, EventArgs e)
    {
        OnHPChangeEventArgs args = e as OnHPChangeEventArgs;
        if (args == null || args.target != BarName) return;

        HP = args.HP;
        UpdateUI();
    }

}
