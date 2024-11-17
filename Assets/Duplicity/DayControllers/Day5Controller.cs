using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day5Controller : DayController
{
    public Dialog[] dialog;
    //bool isFirst = false;
    //�ζ��� �� ��
    public override void Initialize(string currentTask)
    {
        //Day5 �ǳ��� ��ȭ x
        UIManager.Instance.ActiveInventory();
        //������ ���׷� start�� �ӽ�Ȱ��ȭ
        UIManager.Instance.TogglInventoryUI();
        MapManager.Instance.InitializeMapRegions();
    }

    public override void CompleteTask(string task)
    {
        if (task == "WakeUp")
        {
            MarkTaskComplete(task);
            UpdateCurrentTask(task);
        }
        
    }

    public override bool IsDayComplete(string currentTask)
    {
        //gameState�� �����ϴ� Ű������ �ľ�
        return gameState.ContainsKey("WakeUp") && gameState["WakeUp"];
    }

    public override void MapIconClick(string regionName)
    { 

    }
}
