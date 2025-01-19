using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boss : MonoBehaviour
{   

    public int MaxHP=4;
    public int CurrentHP;
    Sequence sequence;

    public void Init()
    {
        CurrentHP = MaxHP;
        OnHPChangeEventArgs args = OnHPChangeEventArgs.Create(CurrentHP, "BossHPBar");
        EventManager.Instance.TriggerEvent(OnHPChangeEventArgs.EventId, this, args);
        //ShakeAnim();
    }

    private void OnEnable() {
        Init();
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
        
        CurrentHP= Mathf.Clamp(CurrentHP + value, 0, MaxHP);
        OnHPChangeEventArgs args = OnHPChangeEventArgs.Create(CurrentHP, "BossHPBar");
        EventManager.Instance.TriggerEvent(OnHPChangeEventArgs.EventId, this, args);
        if (CurrentHP <= 0)
        {
            LevelManager.Instance.LevelSuccess();
        }
    }
}
