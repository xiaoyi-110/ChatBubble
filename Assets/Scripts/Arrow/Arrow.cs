using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    public Sprite[] arrowSprites;  // ��ͷ�����Spriteͼ��
    Image image;

    [HideInInspector]
    public int arrowDir;

    public Color finishColor;  // ��ɺ��ͷ����ɫ
    public Color errorColor;   // ����ʱ��ͷ����ɫ

    void Awake()
    {
        image = GetComponent<Image>();
    }

    // ��ʼ����ͷ����
    public void Setup(int dir)
    {
        arrowDir = dir;
        image.sprite = arrowSprites[dir];
        image.SetNativeSize();
    }

    // ����Ϊ���״̬���ı��ͷ��ɫ
    public void SetFinish()
    {
        image.color = finishColor;
    }

    // ����Ϊ����״̬���ı��ͷ��ɫ
    public void SetError()
    {
        image.color = errorColor;
    }
}