
using UnityEngine;
using DG.Tweening;
public class Bullet : MonoBehaviour
{
    // 欧拉角
    public float Direction=0;
    public float Speed;
    public bool IsAttackable;
    private bool IsBeHit;

    /// <summary>
    /// 击飞动画
    /// </summary>
    // 击飞的目标位置
    public Vector3 targetPosition;
    // 击飞的时间（秒）
    public float duration = 1f;
    // 旋转的角度（每秒）
    public Vector3 rotationSpeed = new Vector3(0, 0, 720); // 旋转的角度（每秒）

    private Sequence squence;


    private void Start() {
        transform.Rotate(new Vector3(0, 0, Direction));   
        
    }

    private void OnEnable() {
        IsAttackable = true;
        IsBeHit = false;
    }


    private void Update() {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }

    public void Init(BulletData data) {
    
        Speed = data.Speed;
        IsAttackable = data.IsAttackable == 1;
    
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Boundary")) {
            ObjectPool.Instance.RecycleObject(gameObject);
        }
    }

    public void BeHit()
    {
        if(!IsAttackable || IsBeHit) return;

        LevelManager.Instance.m_Boss.ChangeHP(-1);
        ObjectPool.Instance.GetObject("BoomEffect", transform.position);
        IsBeHit = true;
        //随机化目标位置
        targetPosition = transform.position + new Vector3(-1f, Random.Range(-1f, 1f), 0) * 10f;
        StartCoroutine(HitAndSpinCoroutine());
    }

    /// <summary>
    /// 击飞动画
    /// </summary>
    /// <returns></returns>
    public System.Collections.IEnumerator HitAndSpinCoroutine()
    {
        
        // 暂停一帧以确保初始状态被正确渲染
        yield return null;

        // DoTween的位置和旋转动画
        squence = DOTween.Sequence();
        squence.Append(
            transform.DOMove(targetPosition, duration)
                    .SetEase(Ease.OutQuad) // 设置缓动类型为抛物线型退出
                    
        );

        //使用DO Shake Rotation 或者直接设置旋转目标
        squence.Join(
            transform.DORotate(rotationSpeed * duration, duration)
                    .SetEase(Ease.Linear).SetRelative().OnComplete(() => 
                    {
                        ObjectPool.Instance.RecycleObject(gameObject);
                    })// 线性旋转
        );
    }


    private void OnDisable() {
        squence.Kill();
    }

}