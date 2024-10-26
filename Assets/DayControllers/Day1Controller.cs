using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day1Controller : DayController
{
    public override void Initialize(string currentTask)
    {
        //Day1 => Intro Initialize 불필요
        Debug.Log("Day1 시작");
        //throw new System.NotImplementedException();
    }
    public override void CompleteTask(string task)
    {
        // 예시
        if (task == "Day1CutScene")
        {
            MarkTaskComplete(task);
        }
        // Task 완료 후 currentTask 업데이트
        UpdateCurrentTask(task);
    }

    public override bool IsDayComplete(string currentTask)
    {
        // 모든 Task가 완료되었는지 확인
        return currentTask == "Day1CutScene";
    }

    public override void MapIconClick(string regionName)
    {
        throw new System.NotImplementedException();
    }
}
