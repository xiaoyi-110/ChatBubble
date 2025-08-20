using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boss : Entity
{   
    public int MaxHP=4;
    public int CurrentHP;
    Sequence sequence;
    protected override void Awake()
    {
        base.Awake();
        EntityRegistry.Register(EntityType.Boss, this);
    }
    public void Init()
    {
        CurrentHP = MaxHP;
        OnHPChangeEventArgs args = OnHPChangeEventArgs.Create(CurrentHP, EntityType.Boss);
        if (EventManager.Instance != null)
        {
            EventManager.Instance.TriggerEvent(OnHPChangeEventArgs.EventId, this, args);
        }

        //ShakeAnim();
    }

    private void OnEnable() {
        ShakeAnim();
    }
    // 摇晃动画
    public void ShakeAnim()
    {
        //transform.DOShakeRotation(3f, new Vector3(0, 0, 10),10 ,10, false).SetLoops(-1, LoopType.Yoyo);
        sequence = DOTween.Sequence();
        sequence.Append(transform.DORotate(new Vector3(0, 0, 20), 1f));
        sequence.SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable() {
        sequence.Kill();
    }

    public void ChangeHP(int value=-1)
    {
        if(value==0)return;

        AudioManager.Instance.Play("hitBoss");
        CurrentHP = Mathf.Clamp(CurrentHP + value, 0, MaxHP);
        OnHPChangeEventArgs args = OnHPChangeEventArgs.Create(CurrentHP, EntityType.Boss);
        EventManager.Instance.TriggerEvent(OnHPChangeEventArgs.EventId, this, args);
        if (CurrentHP <= 0)
        {
            AudioManager.Instance.Play("bossDie");
            LevelManager.Instance.LevelSuccess();
        }
    }
}
