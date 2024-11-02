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
        // 모든 Canvas 비활성화
        foreach (GameObject days in daysAssets)
        {
            if (days != null)
            {
                days.SetActive(false);
            }
        }

        // currentDay에 맞는 Canvas만 활성화
        if (currentDay == 2)
        {
            //2부터 시작
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
            Debug.LogWarning("daysAssets를 찾을 수 없음");
        }
    }
}
