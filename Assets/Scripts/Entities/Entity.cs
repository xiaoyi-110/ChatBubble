using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Collision info")]
    [SerializeField] protected Transform m_GroundCheck;
    [SerializeField] protected float m_CheckGroundDistance = 0.15f;
    [Space]
    [SerializeField] protected Transform m_WallCheck;
    [SerializeField] protected float m_CheckWallDistance = 0.15f;
    [SerializeField] protected LayerMask m_GroundLayer;

    public Rigidbody2D Rb {get; private set;}
    public Animator Animator { get; private set;}
    protected int m_FacingDirection = 1;//1为右，-1为左


    public bool IsGroundDetected() => Physics2D.Raycast(m_GroundCheck.position, Vector2.down, m_CheckGroundDistance, m_GroundLayer);
    public bool IsWallDetected() => Physics2D.Raycast(m_WallCheck.position, Vector2.right, m_CheckWallDistance, m_GroundLayer);

    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Start(){}

    // Update is called once per frame
    protected virtual void Update()
    {
        
        FlipController(Rb.velocity.x);  
    }

    

    public void SetVelocity(float _xVelocity, float _yVelocity)    
    {
        Rb.velocity = new Vector2(_xVelocity, _yVelocity);
    }


    /// <summary>
    /// 翻转控制器
    /// </summary>
    /// <param name="x"></param>
    private void FlipController(float x)
    {
        // 当朝向方向与移动方向相反时，翻转角色
        if (m_FacingDirection * x < 0)
        {
            Flip();
        }
    }

    /// <summary>
    /// 翻转角色
    /// </summary>
    private void Flip()
    {
        m_FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(m_GroundCheck.position, new Vector3(m_GroundCheck.position.x, m_GroundCheck.position.y - m_CheckGroundDistance));
        Gizmos.DrawLine(m_WallCheck.position, new Vector3(m_WallCheck.position.x + m_CheckWallDistance * m_FacingDirection, m_WallCheck.position.y));
    }
    
}
