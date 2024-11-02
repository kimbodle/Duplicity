using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelterController : MonoBehaviour
{
    public int dialogCount = 0;
    private int currentDay = 0;
    [SerializeField] private GameObject[] DayCanvas;

    void Start()
    {
        currentDay = GameManager.Instance.GetCurrentDay();
        ActivateCurrentDayCanvas();

        switch (currentDay)
        {
            case 3:
                dialogCount = 0;
                break;
        }
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
        if (currentDay >= 3)
        {
            if (DayCanvas[currentDay - 3] != null)
            {
                DayCanvas[currentDay - 3].SetActive(true);
            }
        }
        else
        {
            Debug.LogWarning("��ȿ���� ���� currentDay or Canvas�� ã�� �� ����");
        }
    }
}
