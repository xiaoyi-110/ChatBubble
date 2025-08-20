using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Collision info")]
    [SerializeField] protected List<Transform> m_GroundChecks = new List<Transform>();
    [SerializeField] protected float m_CheckGroundDistance = 0.15f;
    [Space]
    [SerializeField] protected Transform m_WallCheck;
    [SerializeField] protected float m_CheckWallDistance = 0.15f;
    [SerializeField] protected LayerMask m_GroundLayer;

    public Rigidbody2D Rb {get; private set;}
    public Animator Animator;
    public int FacingDirection = 1;//1为右，-1为左

    private bool m_IsPause = false;


    public bool IsGroundDetected() => m_GroundChecks.Exists(check => Physics2D.Raycast(check.position, Vector2.down, m_CheckGroundDistance, m_GroundLayer));
    public bool IsWallDetected() => Physics2D.Raycast(m_WallCheck.position, Vector2.right, m_CheckWallDistance, m_GroundLayer);

    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Start(){
        EventManager.Instance.RegisterEvent(OnLevelPauseChangeEventArgs.EventId, OnLevelPauseChange);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(m_IsPause)
        {
            return;
        }

        OnUpdate();
    }

    protected virtual void FixedUpdate() {
        if(m_IsPause)
        {
            return;
        }
    }

    protected virtual void OnUpdate()
    {
        FlipController(Rb.velocity.x);  
    }



    public void SetVelocity(float xVelocity, float? yVelocity = null)
    {
        float newY = yVelocity ?? Rb.velocity.y; 
        Rb.velocity = new Vector2(xVelocity, newY);
    }



    /// <summary>
    /// 翻转控制器
    /// </summary>
    /// <param name="x"></param>
    private void FlipController(float x)
    {
        // 当朝向方向与移动方向相反时，翻转角色
        if (FacingDirection * x < 0)
        {
            Flip();
        }
    }

    /// <summary>
    /// 翻转角色
    /// </summary>
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmos()
    {
        foreach (Transform check in m_GroundChecks)
        {
            Gizmos.DrawLine(check.position, new Vector3(check.position.x, check.position.y - m_CheckGroundDistance));
        }
        //Gizmos.DrawLine(m_WallCheck.position, new Vector3(m_WallCheck.position.x + m_CheckWallDistance * FacingDirection, m_WallCheck.position.y));
    }

    public void OnLevelPauseChange(object sender, EventArgs e)
    {
        OnLevelPauseChangeEventArgs ne = e as OnLevelPauseChangeEventArgs;

        m_IsPause = ne.IsPause;

        if(Animator != null)
        {
            Animator.speed = m_IsPause ? 0 : 1;
        }
        if(Rb != null)
        {
            Rb.simulated = !m_IsPause;
        }
    }
    
}
