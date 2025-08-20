using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ArrowManager : MonoSingleton<ArrowManager>
{
    public GameObject ArrowPrefab;
    public RectTransform ArrowsHolder;
    public static bool IsFinished;

    private Queue<Arrow> _arrows = new Queue<Arrow>();
    private Arrow m_currentArrow;

    public float WaveTime = 9f; // 每一轮的倒计时（秒）  


    public void CreateWave(int length)
    {
        Debug.Log($"正在生成箭头，剩余次数: {length}");
        _arrows.Clear();
        IsFinished = false;

        for (int i = 0; i < length; i++)
        {
            GameObject arrowObject = ObjectPool.Instance.GetObject("Arrow");
            arrowObject.transform.SetParent(ArrowsHolder);
            Arrow arrow = arrowObject.GetComponent<Arrow>();
          
            int randomDirection = Random.Range(0, 4); 
            arrow.Initialize(randomDirection);
            RectTransform rectTransform = arrowObject.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.localPosition = new Vector3(i * 100, 0, 0);
            }
            _arrows.Enqueue(arrow);
        }

        m_currentArrow = _arrows.Dequeue();  
    }
 
    public void HandleArrowInput(KeyCode inputKey)
    {
        if (IsFinished)
            return;

        if (ConvertKeyCodeToDirection(inputKey) == m_currentArrow.ArrowDirection)
        {
            m_currentArrow.SetToFinishState(); 
            AudioManager.Instance.Play("qqHit");
            ArrowPhaseController.Instance.RecordInput(true);  
        }
        else
        {
            m_currentArrow.SetToErrorState(); 
            AudioManager.Instance.Play("qqMiss");
            ArrowPhaseController.Instance.RecordInput(false); 
        }

        // 继续到下一个箭头  
        if (_arrows.Count > 0)
        {
            m_currentArrow = _arrows.Dequeue(); // 继续下一个箭头  
        }
        else
        {
            IsFinished = true; // 如果所有箭头都输入完，标记关卡完成  
        }
    }

    // 清空箭头波  
    public void ClearWave()
    {     
        int recycledCount = 0;
        foreach (Transform arrow in ArrowsHolder)
        {
            ObjectPool.Instance.RecycleObject(arrow.gameObject);
            recycledCount++;
        }
        _arrows.Clear();
        Debug.Log($"回收了箭头波次，数量: {recycledCount}");
    }
  
    private int ConvertKeyCodeToDirection(KeyCode key)
    {
        return key switch
        {
            KeyCode.UpArrow => 0,
            KeyCode.DownArrow => 1,
            KeyCode.LeftArrow => 2,
            KeyCode.RightArrow => 3,
            _ => -1
        };
    }
}
