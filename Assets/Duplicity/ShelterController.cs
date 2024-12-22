using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShelterController : MonoBehaviour
{
    private int currentDay = 0;
    [SerializeField] private GameObject[] DayCanvas;
    private Day8Manager day8Manager;

    void Start()
    {
        currentDay = GameManager.Instance.GetCurrentDay();
        ActivateCurrentDayCanvas();
    }

    private void ActivateCurrentDayCanvas()
    {
        // 모든 Canvas 비활성화
        foreach (GameObject canvas in DayCanvas)
        {
            if (canvas != null)
            {
                canvas.SetActive(false);
            }
        }

        // currentDay에 맞는 Canvas만 활성화
        if (currentDay == 3 || currentDay == 4)
        {
            if (DayCanvas[currentDay - 3] != null)
            {
                DayCanvas[currentDay - 3].SetActive(true);
            }
        }
        else if(currentDay == 6)
        {
            if (DayCanvas[2] != null)
            {
                DayCanvas[2].SetActive(true);
            }
        }
        else if (currentDay == 8)
        {
            if (DayCanvas[3] != null)
            {
                DayCanvas[3].SetActive(true);
            }

            day8Manager = FindObjectOfType<Day8Manager>();

            if (day8Manager != null)
            {
                day8Manager.AddDay8Items();
            }
            else
            {
                Debug.LogWarning("Day8Manager -> null");
            }
        }
        else
        {
            Debug.LogWarning("유효하지 않은 currentDay or Canvas를 찾을 수 없음");
        }
    }
}
