using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day3Controller : DayController
{
    public override void Initialize(string currentTask)
    {
        Debug.Log("Day3 ����");
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
        //gameState Dictionary�� �Ǵ��� �̵� ���� �ֱ�
        //�ƴ� �� DialogueManager�� AdviseMessageDialog ȣ��
        if (regionName == "Shelter")
        {
            GameManager.Instance.CompleteTask();
        }
    }

}