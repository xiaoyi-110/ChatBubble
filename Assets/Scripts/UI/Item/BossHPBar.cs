using System;
using UnityEngine;
using UnityEngine.UI;
public class BossHPBar : MonoBehaviour
{
    public EntityType BarName;
    public int MaxHP;
    public int HP;
    public Image HPValue;

    private void Awake() {
        
    }

    void Start()
    {       
        EventManager.Instance.RegisterEvent(OnHPChangeEventArgs.EventId, OnHpChange);

        int initialHP = BarName == EntityType.Player
                ? LevelManager.Instance.Player.MaxHP
                : LevelManager.Instance.Boss.MaxHP;

        Init(initialHP);

    }

    public void Init(int hp)
    {
        MaxHP = HP = hp;
    }


    
    public void OnHpChange(object sender, EventArgs e)
    {
        OnHPChangeEventArgs args = e as OnHPChangeEventArgs;
        if (args == null || args.target != BarName) return;

        HP = args.HP;
        HPValue.fillAmount = (float)HP / MaxHP;
    }
}