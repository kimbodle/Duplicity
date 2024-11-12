using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day7Controller : DayController
{
    public override void Initialize(string currentTask)
    {
        //일단 후속 처리x
        Debug.Log("Day7 시작");
    }

    public override void CompleteTask(string task)
    {
        if (task == "day7Complete")
        {
            MarkTaskComplete(task);
            UpdateCurrentTask(task);
        }
    }
       

    public override bool IsDayComplete(string currentTask)
    {
        //gameState에 존재하는 키값으로 파악
        return gameState.ContainsKey("day7Complete") && gameState["day7Complete"];
    }

    public override void MapIconClick(string regionName)
    {
        if (SceneManager.GetActiveScene().name != regionName)
        {
            if (regionName == "LibraryScene" || regionName == "LaboratoryScene")
            {
                DialogManager.Instance.AdviseMessageDialog(1);
            }
            if (regionName == "ShelterScene")
            {
                //모든 task 를 안끝냈으면
                if (IsDayComplete(GameManager.Instance.currentTask))
                {
                    //NextDay(Day8) & 피난처로 이동
                    GameManager.Instance.CompleteTask("ShelterScene");
                }
                else
                {
                    DialogManager.Instance.AdviseMessageDialog(1);
                }
            }
        }
        else
        {
            Debug.Log("지금 있는 곳");
        }
    }
}
