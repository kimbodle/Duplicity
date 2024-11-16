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
        UIManager.Instance.DeactiveMapIcon();
        UIManager.Instance.ActiveInventory();
    }



    public override void CompleteTask(string task)
    {
        if (task == "ItemCollected")
        {
            MarkTaskComplete(task);
        }
    }

    
    public override bool IsDayComplete(string currentTask)
    {
        return gameState.ContainsKey("ItemCollected") && gameState["ItemCollected"];
    }

    public override void MapIconClick(string regionName)
    {
        
    }
}
