using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boss : MonoBehaviour
{
    Sequence sequence;
    private void OnEnable() {
        ShakeAnim();
    }
    // 摇晃动画
    public void ShakeAnim()
    {
        //transform.DOShakeRotation(3f, new Vector3(0, 0, 10),10 ,10, false).SetLoops(-1, LoopType.Yoyo);
        sequence = DOTween.Sequence();
        sequence.Append(transform.DORotate(new Vector3(0, 0, 20), 1f));
        sequence.SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable() {
        sequence.Kill();
    }
}
