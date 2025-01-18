
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("玩家属性")]
    public int MaxHP = 3;
    public float InvincibleTimeWindow = 1f;

    [Header("玩家当前状态")]
    public int CurrentHP;
    
    public float InvincibleTimer;

    

    private void Start()
    {

        CurrentHP = MaxHP;
    }

    public void ChangeHP(int value=-1)
    {
        if(value==0)return;
        
        CurrentHP= Mathf.Clamp(CurrentHP + value, 0, MaxHP);
        OnPlayerHPChangeEventArgs args = OnPlayerHPChangeEventArgs.Create(CurrentHP);
        EventManager.Instance.TriggerEvent(OnPlayerHPChangeEventArgs.EventId, this, args);
        
        if (CurrentHP <= 0)
        {
            //TODO: 游戏结束
        }
    }

}
