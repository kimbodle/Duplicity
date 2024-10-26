using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day1Controller : DayController
{
    public override void Initialize(string currentTask)
    {
        //Day1 => Intro Initialize ���ʿ�
        Debug.Log("Day1 ����");
        //throw new System.NotImplementedException();
    }
    public override void CompleteTask(string task)
    {
        // ����
        if (task == "Day1CutScene")
        {
            MarkTaskComplete(task);
        }
        // Task �Ϸ� �� currentTask ������Ʈ
        UpdateCurrentTask(task);
    }

    public override bool IsDayComplete(string currentTask)
    {
        // ��� Task�� �Ϸ�Ǿ����� Ȯ��
        return currentTask == "Day1CutScene";
    }

    public override void MapIconClick(string regionName)
    {
        throw new System.NotImplementedException();
    }
}
