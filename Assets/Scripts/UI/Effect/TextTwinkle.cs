using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TextTwinkle : MonoBehaviour 
{
    private Text m_Text;
    private Sequence sequence;
    private void Start() {
        m_Text = GetComponent<Text>();
        sequence = DOTween.Sequence();
        sequence.Append(m_Text.DOFade(0.2f, 1.5f));
        sequence.Append(m_Text.DOFade(0.9f, 1f));
        sequence.SetLoops(-1);
    }


    private void OnDestroy() {
        if (sequence != null) {
            sequence.Kill();
        }
    }
}