using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    public float offset = 100f;
    public Text text;
    private BoxCollider2D m_Collider;
    private RectTransform rectTransform;

    [Header("BoxCollider2D")]
    public float box_offset = 20f;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        m_Collider = GetComponent<BoxCollider2D>();
        
    }

    private void Update()
    {
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, text.preferredWidth+offset);
        m_Collider.size = new Vector2(text.preferredWidth + box_offset, rectTransform.rect.height);
    }

    public void Init(BulletData data)
    {
        text.text = data.Text;
    }


}
