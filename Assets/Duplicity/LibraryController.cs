using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryController : MonoBehaviour
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
        if (currentDay == 3)
        {
            if (daysAssets[currentDay - 3] != null)
            {
                daysAssets[currentDay - 3].SetActive(true);
            }
        }
        else
        {
            Debug.LogWarning("daysAssets�� ã�� �� ����");
        }
    }
}
