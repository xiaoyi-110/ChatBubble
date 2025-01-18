
using System.Diagnostics;
using UnityEngine;
using UnityEngine.PlayerLoop;
public class Bullet : MonoBehaviour
{
    public enum BulletType:int {
        DestructiveBullet,
        Bullet
    }

    public BulletType Type;
    // 欧拉角
    public float Direction=0;
    public float Speed;

    private BoxCollider2D m_Collider;

    private void Start() {
        m_Collider = GetComponent<BoxCollider2D>();
        transform.Rotate(new Vector3(0, 0, Direction));
    }


    private void Update() {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }

    public void Init(BulletData data) {
    
        Type = (BulletType)data.Type;
        Speed = data.Speed;
    
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Boundary")) {
            Destroy(gameObject);
        }
    }

}