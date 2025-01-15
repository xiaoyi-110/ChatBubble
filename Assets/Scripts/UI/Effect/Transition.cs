
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Transition : MonoBehaviour
{
    private Image image;
    public float time = 1f;

    private void Awake() {
        image = GetComponent<Image>();
    }


    public void FadeIn(ProcedureChangeScene procedureChangeScene)
    {
        
        gameObject.SetActive(true);
        image.DOFade(1, time).onComplete = () =>
        {
            procedureChangeScene.OnTransitionFadeInComplete();
        };
    }

    public void FadeOut()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(0.5f);
        sequence.Append(image.DOFade(0, time));
        sequence.onComplete = () =>
        {
            gameObject.SetActive(false);
        };
        
    }
}
