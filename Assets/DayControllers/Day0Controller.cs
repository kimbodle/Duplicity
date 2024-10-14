using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day0Controller : DayController
{
    private void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public override void Initialize(string currentTask)
    {
        //Day0 => Intro Initialize 불필요
        Debug.Log("Day0 시작");
        //throw new System.NotImplementedException();
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

}
