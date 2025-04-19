using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day9Controller : DayController
{
    public override void Initialize(string currentTask)
    {
        Debug.Log("Day9 ����");
        
        //�� ������ ��Ȱ��ȭ
        UIManager.Instance.DeactivateMapIcon();
    }



    public override void CompleteTask(string task)
    {
        if (task == "TheEnd")
        {
            MarkTaskComplete(task);
            UpdateCurrentTask(task);
        }
    }

    
    public override bool IsDayComplete(string currentTask)
    {
        return gameState.ContainsKey("TheEnd") && gameState["TheEnd"];
    }

    public override void MapIconClick(string regionName)
    {
        
    }
}
