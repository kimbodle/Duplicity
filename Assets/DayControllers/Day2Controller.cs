using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Day2Controller : DayController
{
    public override void Initialize(string currentTask)
    {
        //�ʱ�ȭ ���ʿ�
        Debug.Log("Day2 ����");
    }

    public override void CompleteTask(string task)
    {
        if (task == "ItemCollected")
        {
            MarkTaskComplete(task);
        }
        // Task �Ϸ� �� currentTask ������Ʈ
        UpdateCurrentTask(task);
    }

    public override bool IsDayComplete(string currentTask)
    {
        return currentTask == "ItemCollected";
    }

    public override void MapIconClick(string regionName)
    {
        Debug.Log("MapClick �ƾ��");
        if(regionName == "Shelter")
        {
            GameManager.Instance.CompleteTask();
        }
    }
}
