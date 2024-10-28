using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day3Controller : DayController
{
    public override void Initialize(string currentTask)
    {
        Debug.Log("Day3 시작");
    }

    public override void CompleteTask(string task)
    {
        if (task == "")
        {
            MarkTaskComplete(task);
        }
        UpdateCurrentTask(task);
    }

    public override bool IsDayComplete(string currentTask)
    {
        return currentTask == "";
    }

    public override void MapIconClick(string regionName)
    {
        //gameState Dictionary로 판단해 이동 제한 넣기
        //아닐 시 DialogueManager의 AdviseMessageDialog 호출
        if (regionName == "Shelter")
        {
            GameManager.Instance.CompleteTask();
        }
    }

}
