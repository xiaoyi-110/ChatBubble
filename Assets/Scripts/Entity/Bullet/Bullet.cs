
using System.Diagnostics;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    public enum BulletType {
        DestructiveBullet,
        Bullet
    }

    public BulletType Type;
    // 欧拉角
    public float Direction;
    public float Speed;

    private BoxCollider2D m_Collider;

    private void Start() {
        m_Collider = GetComponent<BoxCollider2D>();
        transform.Rotate(new Vector3(0, 0, Direction));
    }


    private void Update() {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }

}