

using System;
using UnityEngine;
using UnityEngine.UI;
public class BossHPBar : MonoBehaviour
{
    public string BarName;
    public int MaxHP;
    public int HP;
    public Image HPValue;

    private void Awake() {
        
    }

    void Start()
    {
        

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
        //show();
    }


    
    public void OnHpChange(object sender, EventArgs e)
    {
        OnHPChangeEventArgs args = e as OnHPChangeEventArgs;
        if(args.target != BarName)return;
        int hp = args.HP;
        HPValue.fillAmount = (float)hp / MaxHP;
    }
}