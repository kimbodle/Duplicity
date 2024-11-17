using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day5Controller : DayController
{
    public Dialog[] dialog;
    //bool isFirst = false;
    //로라의 꿈 속
    public override void Initialize(string currentTask)
    {
        //Day5 피난묘 대화 x
        UIManager.Instance.ActiveInventory();
        //아이콘 버그로 start때 임시활성화
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
        //gameState에 존재하는 키값으로 파악
        return gameState.ContainsKey("WakeUp") && gameState["WakeUp"];
    }

    public override void MapIconClick(string regionName)
    { 

    }
}
