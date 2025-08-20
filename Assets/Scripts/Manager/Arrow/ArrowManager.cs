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

    public float WaveTime = 9f; // ÿһ�ֵĵ���ʱ���룩  


    public void CreateWave(int length)
    {
        Debug.Log($"�������ɼ�ͷ��ʣ�����: {length}");
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

        // ��������һ����ͷ  
        if (_arrows.Count > 0)
        {
            m_currentArrow = _arrows.Dequeue(); // ������һ����ͷ  
        }
        else
        {
            IsFinished = true; // ������м�ͷ�������꣬��ǹؿ����  
        }
    }

    // ��ռ�ͷ��  
    public void ClearWave()
    {     
        int recycledCount = 0;
        foreach (Transform arrow in ArrowsHolder)
        {
            ObjectPool.Instance.RecycleObject(arrow.gameObject);
            recycledCount++;
        }
        _arrows.Clear();
        Debug.Log($"�����˼�ͷ���Σ�����: {recycledCount}");
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
