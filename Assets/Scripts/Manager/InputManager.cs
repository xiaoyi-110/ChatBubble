using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    /*public static InputManager S;
    private float inputTimeLimit = 9.0f; // Time limit in seconds
    private float inputStartTime;

    private void Awake()
    {
        S = this;
    }

    void Update()
    {
        if (!GameManager.isDancing || Time.time - inputStartTime > inputTimeLimit)
        {
            return;
        }

        if (Input.anyKeyDown)
        {
            inputStartTime = Time.time; // Reset timer on any key press
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
            ArrowManager.S.TypeArrow(KeyCode.UpArrow);

        if (Input.GetKeyDown(KeyCode.DownArrow))
            ArrowManager.S.TypeArrow(KeyCode.DownArrow);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            ArrowManager.S.TypeArrow(KeyCode.LeftArrow);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            ArrowManager.S.TypeArrow(KeyCode.RightArrow);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (ArrowManager.isFinish)
            {
                GameManager.S.FinishWave();
            }
            else
            {
                GameManager.S.FailWave();
            }
        }
    }*/
}