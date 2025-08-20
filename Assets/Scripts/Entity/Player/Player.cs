
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : Entity
{
    [Header("玩家属性")]
    public int MaxHP = 3;
    public float FloatUpSpeed = 5f;
    public float FloatDownSpeed = 4f;
    public float InvincibleTimeWindow = 1f;
    private Vector2 m_InitPosition;

    [Header("玩家当前状态")]
    public int CurrentHP;
    private float invincibleTimer;
    public bool IsHurtInvincible => invincibleTimer > 0f;
    private bool skillInvincible;
    public bool IsSkillInvincible => skillInvincible;
    public bool IsInvincible => IsHurtInvincible || IsSkillInvincible;


    [Header("碰撞检测")]
    public Rigidbody2D RD;
    public LayerMask GroundLayer;
    public Transform GroundCheck;
    public float GroundCheckLen = 1f;
    public Transform CeilingCheck;
    public float CeilingCheckLen = 1f;
    public Transform AttackCheck;
    public float AttackRadius = 1f;

    public bool IsGrounded => Physics2D.Raycast(GroundCheck.position, Vector2.down, GroundCheckLen, GroundLayer);
    public bool IsCeiling => Physics2D.Raycast(CeilingCheck.position, Vector2.up, CeilingCheckLen, GroundLayer);

    #region FSM  
    private FSM<Player> m_FSM;
    private List<FSMState<Player>> m_FSMStates;
    #endregion

    #region Control  
    public bool IsTryJump => Input.GetKeyDown(KeyCode.Space);
    public bool IsTryAir => Input.GetKey(KeyCode.Space);
    public bool IsTryAttack => Input.GetKeyDown(KeyCode.Space);
    #endregion

    protected override void Awake()
    {
        base.Awake();
        EntityRegistry.Register(EntityType.Player, this);
        m_InitPosition = transform.position;
        CreateFSM();
    }

    private void OnEnable()
    {
        //Init();
    }

    // 初始化  
    public void Init()
    {
        CurrentHP = MaxHP;
        invincibleTimer = -1f;
        transform.position = m_InitPosition;

        InitFSM();

        OnHPChangeEventArgs args = OnHPChangeEventArgs.Create(CurrentHP, EntityType.Player);
        if (EventManager.Instance != null)
        {
            EventManager.Instance.TriggerEvent(OnHPChangeEventArgs.EventId, this, args);
        }
    }

    private void CreateFSM()
    {
        m_FSMStates = new List<FSMState<Player>>()
       {
           PlayerIdleState.Create("Idle"),
           PlayerJumpState.Create("Jump"),
           PlayerAirState.Create("Air"),
           PlayerFallState.Create("Fall"),
           PlayerAttackedState.Create("Attacked"),
       };

        m_FSM = new FSM<Player>(this, m_FSMStates);
        m_FSM.StartState<PlayerIdleState>();
    }

    private void InitFSM()
    {
        m_FSM.StartState<PlayerIdleState>();
    }


    protected override void Update()
    {
        if (!LevelManager.Instance.IsGameStarted) return;
        m_FSM.OnUpdate();
        if (invincibleTimer > 0f)
        {
            invincibleTimer -= Time.deltaTime;
        }
    }

    public void SetInvincible(bool value)
    {
        skillInvincible = value;
    }



    public void ChangeHP(int value = -1)
    {
        if (invincibleTimer >= 0 || value == 0) return;
        if (value < 0) invincibleTimer = InvincibleTimeWindow;

        CurrentHP = Mathf.Clamp(CurrentHP + value, 0, MaxHP);
        OnHPChangeEventArgs args = OnHPChangeEventArgs.Create(CurrentHP, EntityType.Player);
        EventManager.Instance.TriggerEvent(OnHPChangeEventArgs.EventId, this, args);

        if (CurrentHP <= 0)
        {
            AudioManager.Instance.Play("playerDie");
            LevelManager.Instance.LevelOver();
        }
    }

    public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(AttackCheck.transform.position, AttackRadius);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Bullet"))
            {
                collider.GetComponent<Bullet>().BeHit();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(GroundCheck.position, GroundCheck.position + Vector3.down * GroundCheckLen);
        Gizmos.DrawLine(CeilingCheck.position, CeilingCheck.position + Vector3.up * CeilingCheckLen);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackCheck.position, AttackRadius);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet") && !IsInvincible && !other.GetComponent<Bullet>().IsAttackable)
        {
            ChangeHP(-1);
            AudioManager.Instance.Play("hitPlayer");
        }
    }
}
