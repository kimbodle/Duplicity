using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaboratoryController : MonoBehaviour
{
    private int currentDay = 0;
    [SerializeField] private GameObject[] daysAssets;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            currentDay = GameManager.Instance.GetCurrentDay();
            ActivateCurrentDayState();
        }
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
            if (daysAssets[0] != null)
            {
                daysAssets[0].SetActive(true);
            }
        }
        else if (currentDay == 4)
        {
            //Day4
            if (daysAssets[1] != null)
            {
                daysAssets[1].SetActive(true);
            }
        }
        else if (currentDay == 8)
        {
            //Day8
            if (daysAssets[2] != null)
            {
                daysAssets[2].SetActive(true);
                if(UIManager.Instance != null)
                {
                    UIManager.Instance.ActiveInventory();
                    UIManager.Instance.TogglInventoryUI();
                }
            }
        }
        else
        {
            Debug.LogWarning("daysAssets�� ã�� �� ����");
        }
    }
}
