using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelterController : MonoBehaviour
{
    private int currentDay = 0;
    [SerializeField] private GameObject[] DayCanvas;

    void Start()
    {
        currentDay = GameManager.Instance.GetCurrentDay();
        ActivateCurrentDayCanvas();
    }

    private void ActivateCurrentDayCanvas()
    {
        // ��� Canvas ��Ȱ��ȭ
        foreach (GameObject canvas in DayCanvas)
        {
            if (canvas != null)
            {
                canvas.SetActive(false);
            }
        }

        // currentDay�� �´� Canvas�� Ȱ��ȭ
        if (currentDay == 3 && currentDay == 4)
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
        else
        {
            Debug.LogWarning("��ȿ���� ���� currentDay or Canvas�� ã�� �� ����");
        }
    }
}
