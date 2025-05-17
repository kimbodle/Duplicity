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
        // ����
        if (task == "Intro")
        {
            MarkTaskComplete(task);
        }
        // Task �Ϸ� �� currentTask ������Ʈ
        UpdateCurrentTask(task);
    }

    public override bool IsDayComplete(string currentTask)
    {
        // ��� Task�� �Ϸ�Ǿ����� Ȯ��
        return currentTask == "Intro";
    }

    public override void MapIconClick(string regionName)
    {
        throw new System.NotImplementedException();
    }
}
