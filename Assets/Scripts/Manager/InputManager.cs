using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager S;

    private void Awake()
    {
        S = this;
    }

    void Update()
    {
        if (!LevelManager.isDancing)
        {
            return;  // �����Ϸ��ͣ������������
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.W))
            ArrowManager.S.TypeArrow(KeyCode.UpArrow);

        if (Input.GetKeyDown(KeyCode.DownArrow)||Input.GetKeyDown(KeyCode.S))
            ArrowManager.S.TypeArrow(KeyCode.DownArrow);

        if (Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.A))
            ArrowManager.S.TypeArrow(KeyCode.LeftArrow);

        if (Input.GetKeyDown(KeyCode.RightArrow)|| Input.GetKeyDown(KeyCode.D))
            ArrowManager.S.TypeArrow(KeyCode.RightArrow);

        // �����Ұ��»س�������ʾ������ǰ�ؿ�
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LevelManager.S.FinishWave();  // ��ɹؿ�

        }
    }
}