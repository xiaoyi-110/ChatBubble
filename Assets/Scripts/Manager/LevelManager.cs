
using System.Diagnostics;
using Cinemachine;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] private Player m_Player;
    [SerializeField] private CinemachineVirtualCamera m_Camera;
    [SerializeField] private CinemachineConfiner2D m_Confiner;
    private Vector3 m_PlayerStartPosition;

    private bool m_IsPause;
    public bool IsPause
    {
        set
        {
            if(m_IsPause != value)
            {
                m_IsPause = value;
                EventManager.Instance.TriggerEvent(OnLevelPauseChangeEventArgs.EventId, this, OnLevelPauseChangeEventArgs.Create(m_IsPause));
            }
        }
        get
        {
            return m_IsPause;
        }
    }

    private void Start()
    { 
        
        IsPause = true;
    }

    public void InitLevel()
    {
        
        m_PlayerStartPosition = GameObject.Find(Constant.LevelData.PlayerStartPosition).transform.position;
        m_Player.transform.position = m_PlayerStartPosition;
        //ChangeCameraPositionImmediately(m_PlayerStartPosition);
        m_Player.gameObject.SetActive(true);

        IsPause = false;

        //InitCamera();
    }

    private void InitCamera()
    {
        SwitchCameraBorder();
        
    }

    public void ExitLevel()
    {
        m_Player.transform.position = Vector3.zero;
        //ChangeCameraPositionImmediately(Vector3.zero);
        m_Player.gameObject.SetActive(false);
        IsPause = true;
    }

    private void SwitchCameraBorder()
    {
        m_Confiner.m_BoundingShape2D = GameObject.Find(Constant.LevelData.CameraBorder).GetComponent<PolygonCollider2D>();
        if (m_Confiner.m_BoundingShape2D == null)
        {
            UnityEngine.Debug.LogError("Camera border not found");
        }

        m_Confiner.InvalidateCache();
    }

    private void ChangeCameraPositionImmediately(Vector3 position)
    {
        CinemachineBrain brain = Camera.main.GetComponent<CinemachineBrain>();
        brain.enabled = false;
        m_Camera.ForceCameraPosition(position, m_Camera.transform.rotation);
        brain.enabled = true;
    }
}