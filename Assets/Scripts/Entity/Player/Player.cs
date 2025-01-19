
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("玩家属性")]
    public int MaxHP = 3;
    public float UpDownSpeed = 10f;
    public float InvincibleTimeWindow = 1f;
    private Vector2 m_InitPosition;

    [Header("玩家当前状态")]
    public int CurrentHP;
    
    public float InvincibleTimer;

    public Animator m_Animator;
    public Rigidbody2D m_RD;

    [Header("碰撞检测")]
    public LayerMask GroundLayer;
    public Transform GroundCheck;
    public float GroundCheckLen = 1f;
    public Transform CeilingCheck;
    public float CeillingCheckLen = 1f;
    public Transform AttackCheck;
    public float AttackRadius = 1f;

    public bool IsGrounded => Physics2D.Raycast(GroundCheck.position, Vector2.down, GroundCheckLen, GroundLayer);
    public bool IsCeiling => Physics2D.Raycast(CeilingCheck.position, Vector2.up, CeillingCheckLen, GroundLayer);
    public bool IsInvincible;
    
    #region FSM

    private FSM<Player> m_FSM;
    private List<FSMState<Player>> fSMStates;
    #endregion

    #region Control
    public bool isTryJump => Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
    public bool isTryAir => Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
    public bool isTryAttack => Input.GetKeyDown(KeyCode.Space);
    #endregion
    

   

    private void Awake()
    {
        m_Animator = GetComponentInChildren<Animator>();
        m_RD = GetComponentInChildren<Rigidbody2D>();   
        m_InitPosition = transform.position;
    }

    private void OnEnable()
    {
        Init();
    }

    // 初始化
    private void Init()
    {
        CurrentHP = MaxHP;
        InvincibleTimer = -1f;
        IsInvincible = false;
        transform.position = m_InitPosition;
        CreateFSM();
    }

    private void CreateFSM()
    {
        fSMStates = new List<FSMState<Player>>()
        {
            PlayerIdleState.Create("Idle"),
            PlayerJumpState.Create("Jump"),
            PlayerAirState.Create("Air"),
            PlayerFallState.Create("Fall"),
            PlayerAttackedState.Create("Attacked"),

        };
        
        m_FSM = new FSM<Player>(this ,fSMStates);

        m_FSM.StartState<PlayerIdleState>();
    }

    private void Update() {
        m_FSM.OnUpdate();
        
        InvincibleTimer -= Time.deltaTime;
    }

    public void ChangeHP(int value=-1)
    {
        if(InvincibleTimer>=0 || value==0)return;
        if(value<0) InvincibleTimer = InvincibleTimeWindow;
        
        CurrentHP= Mathf.Clamp(CurrentHP + value, 0, MaxHP);
        OnPlayerHPChangeEventArgs args = OnPlayerHPChangeEventArgs.Create(CurrentHP);
        EventManager.Instance.TriggerEvent(OnPlayerHPChangeEventArgs.EventId, this, args);
        
        if (CurrentHP <= 0)
        {
            //TODO: 游戏结束
        }
    }

    public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(AttackCheck.transform.position, AttackRadius);
        foreach(var collider in colliders)
        {
            if (collider.CompareTag("Bullet"))
            {
                collider.GetComponent<Bullet>().BeHit();
            }
        }
    }

    

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(GroundCheck.position, GroundCheck.position + Vector3.down * GroundCheckLen);
        Gizmos.DrawLine(CeilingCheck.position, CeilingCheck.position + Vector3.up * CeillingCheckLen);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackCheck.position, AttackRadius);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet") && !IsInvincible && !other.GetComponent<Bullet>().IsAttackable)
        {
            ChangeHP(-1);
        }
    }
       
}
