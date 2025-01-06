using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float checkGroundDistance = 0.15f;
    [Space]
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float checkWallDistance = 0.15f;
    [SerializeField] protected LayerMask groundLayer;

    public Rigidbody2D rb {get; private set;}
    public Animator animator { get; private set;}
    protected int facingDirection = 1;//1为右，-1为左


    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, checkGroundDistance, groundLayer);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right, checkWallDistance, groundLayer);

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Start(){}

    // Update is called once per frame
    protected virtual void Update()
    {
        
        FlipController(rb.velocity.x);  
    }

    

    public void SetVelocity(float _xVelocity, float _yVelocity)    
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
    }


    /// <summary>
    /// 翻转控制器
    /// </summary>
    /// <param name="x"></param>
    private void FlipController(float x)
    {
        // 当朝向方向与移动方向相反时，翻转角色
        if (facingDirection * x < 0)
        {
            Flip();
        }
    }

    /// <summary>
    /// 翻转角色
    /// </summary>
    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - checkGroundDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + checkWallDistance * facingDirection, wallCheck.position.y));
    }
    
}
