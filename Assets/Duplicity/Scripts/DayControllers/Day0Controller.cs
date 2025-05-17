using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day0Controller : DayController
{
    public override void Initialize(string currentTask)
    {
    }
    public override void CompleteTask(string task)
    {
        // 예시
        if (task == "Intro")
        {
            MarkTaskComplete(task);
        }
        // Task 완료 후 currentTask 업데이트
        UpdateCurrentTask(task);
    }

    public override bool IsDayComplete(string currentTask)
    {
        // 모든 Task가 완료되었는지 확인
        return currentTask == "Intro";
    }

    public override void MapIconClick(string regionName)
    {
        throw new System.NotImplementedException();
    }
}
