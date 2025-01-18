using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnceEffect : MonoBehaviour
{
    public void AnimationFinished()
    {
        ObjectPool.Instance.RecycleObject(gameObject);
    }
}
