using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!ArrowPhaseController.IsDancing)
        {
            return;  // 如果游戏暂停，不处理输入
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            ArrowManager.Instance.HandleArrowInput(KeyCode.UpArrow);

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            ArrowManager.Instance.HandleArrowInput(KeyCode.DownArrow);

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            ArrowManager.Instance.HandleArrowInput(KeyCode.LeftArrow);

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            ArrowManager.Instance.HandleArrowInput(KeyCode.RightArrow);

    }
}
