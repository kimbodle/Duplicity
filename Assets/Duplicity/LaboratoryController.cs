using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaboratoryController : MonoBehaviour
{
    private int currentDay = 0;
    [SerializeField] private GameObject[] daysAssets;
    // Start is called before the first frame update
    void Start()
    {
        currentDay = GameManager.Instance.GetCurrentDay();
        ActivateCurrentDayState();
    }

    private void ActivateCurrentDayState()
    {
        // ��� Canvas ��Ȱ��ȭ
        foreach (GameObject days in daysAssets)
        {
            if (days != null)
            {
                days.SetActive(false);
            }
        }

        // currentDay�� �´� Canvas�� Ȱ��ȭ
        if (currentDay == 2)
        {
            //2���� ����
            if (daysAssets[currentDay - 2] != null)
            {
                daysAssets[currentDay - 2].SetActive(true);
            }
        }
        if (currentDay == 4)
        {
            //Day4
            if (daysAssets[1] != null)
            {
                daysAssets[1].SetActive(true);
            }
        }
        else
        {
            Debug.LogWarning("daysAssets�� ã�� �� ����");
        }
    }
}
