using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Day2Controller : DayController
{
    public override void Initialize(string currentTask)
    {
        //초기화 불필요
        Debug.Log("Day2 시작");
    }

    public override void CompleteTask(string task)
    {
        if (task == "ItemCollected")
        {
            MarkTaskComplete(task);
        }
        // Task 완료 후 currentTask 업데이트
        UpdateCurrentTask(task);
    }

    public override bool IsDayComplete(string currentTask)
    {
        return currentTask == "ItemCollected";
    }

    public override void MapIconClick(string regionName)
    {
        Debug.Log("MapClick 됐어요");
        if(regionName == "Shelter")
        {
            GameManager.Instance.CompleteTask();
        }
    }
}
