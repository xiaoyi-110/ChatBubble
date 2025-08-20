using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    public Sprite[] ArrowSprites;
    private Image m_image;

    [HideInInspector]
    public int ArrowDirection;

    public Color FinishColor;
    public Color ErrorColor;
    public Color DefaultColor = Color.white;

    private void Awake()
    {
        m_image = GetComponent<Image>();
    }

    public void Initialize(int direction)
    {
        ResetState();

        ArrowDirection = direction;

        if (ArrowSprites != null && direction >= 0 && direction < ArrowSprites.Length)
        {
            m_image.sprite = ArrowSprites[direction];
            m_image.SetNativeSize();
        }
    }
    public void ResetState()
    {
        m_image.color = DefaultColor;

        m_image.sprite = null;
    }
    public void SetToFinishState()
    {
        m_image.color = FinishColor;
    }

    public void SetToErrorState()
    {
        m_image.color = ErrorColor;
    }
}
