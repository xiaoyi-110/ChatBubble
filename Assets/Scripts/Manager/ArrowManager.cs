using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    /*public static ArrowManager S;
    private void Awake()
    {
        S = this;
    }

    public GameObject arrowPrefab;
    public Transform arrowsHolder;
    public static bool isFinish;

    List<Arrow> arrows = new List<Arrow>(); // Use a list to keep the order and access by index
    private int currentArrowIndex; // Track the current arrow index

    private float inputStartTime; // Start time for the input timer
    private const float inputTimeLimit = 9.0f; // Time limit in seconds

    public void CreateWave(int level)
    {
        arrows.Clear();
        isFinish = false;
        currentArrowIndex = 0; // Reset the index when creating a new wave
        inputStartTime = Time.time; // Reset the timer when starting a new wave

        for (int i = 0; i < 9; i++) // Always create 9 arrows per wave
        {
            Arrow arrow = Instantiate(arrowPrefab, arrowsHolder).GetComponent<Arrow>();
            int randomDir = Random.Range(0, 4);
            arrow.Setup(randomDir);

            arrows.Add(arrow); // Add the arrow to the list instead of queue
        }
    }

    public void TypeArrow(KeyCode inputKey)
    {
        // Check if the player has exceeded the time limit
        if (Time.time - inputStartTime > inputTimeLimit || isFinish)
        {
            GameManager.S.FailWave(); // Input time expired or already finished
            return;
        }

        if (currentArrowIndex < arrows.Count && currentArrowIndex >= 0)
        {
            Arrow currentArrow = arrows[currentArrowIndex];
            if (ConvertKeyCodeToInt(inputKey) == currentArrow.arrowDir)
            {
                currentArrow.SetFinish(); // Change the arrow color
                currentArrowIndex++; // Move to the next arrow

                if (currentArrowIndex >= arrows.Count)
                {
                    isFinish = true;
                    HandleWaveCompletion();
                }
            }
            else
            {
                GameManager.S.FailWave(); // Incorrect input, call FailWave
            }
        }
    }

    public void ClearWave()
    {
        foreach (Transform arrow in arrowsHolder)
        {
            Destroy(arrow.gameObject);
        }
        arrows.Clear();
        currentArrowIndex = 0; // Reset the index after clearing the wave
        isFinish = false; // Reset finish flag
    }

    int ConvertKeyCodeToInt(KeyCode key)
    {
        int result = 0;
        switch (key)
        {
            case KeyCode.UpArrow:
                result = 0;
                break;
            case KeyCode.DownArrow:
                result = 1;
                break;
            case KeyCode.LeftArrow:
                result = 2;
                break;
            case KeyCode.RightArrow:
                result = 3;
                break;
        }
        return result;
    }

    private void HandleWaveCompletion()
    {
        Debug.Log("所有箭头都处理完毕");
        // Do not clear arrows immediately, instead wait for player confirmation or after a certain time
        Invoke("ClearWave", 3); // For example, wait 3 seconds before clearing
    }*/
}