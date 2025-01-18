using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

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
        m_Collider.size = new Vector2(text.preferredWidth + box_offset, m_Collider.size.y);
    }



}
