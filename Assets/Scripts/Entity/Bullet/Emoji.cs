
using UnityEngine;
using UnityEngine.UI;


public class Emoji : MonoBehaviour
{
    public Image image;

    private static Sprite[] m_Sprites;
    public static Sprite[] Sprites
    {
        get
        {
            if (m_Sprites == null)
            {
                m_Sprites = Resources.LoadAll<Sprite>("Sprites/Bullet/Emoji");
            }
            return m_Sprites;
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
    }

    public void Init(BulletData data)
    {
        // 随机读取sprite
        image.sprite = Sprites[Random.Range(0, Sprites.Length)];
    }


}
