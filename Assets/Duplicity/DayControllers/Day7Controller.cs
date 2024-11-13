using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day7Controller : DayController
{
    private Day7MissionManager missionManager;
    public override void Initialize(string currentTask)
    {
        //일단 후속 처리x
        Debug.Log("Day7 시작");

        UIManager.Instance.ActiveInventory();
        UIManager.Instance.ActiveMapIcon();
        //아이콘 버그로 start때 임시활성화
        UIManager.Instance.TogglInventoryUI();
        MapManager.Instance.InitializeMapRegions();

        MapManager.Instance.UnlockRegion("LibraryScene");
        MapManager.Instance.UnlockRegion("LaboratoryScene");
        MapManager.Instance.UnlockRegion("ShelterScene");

        missionManager = FindObjectOfType<Day7MissionManager>();
    }

    public override void CompleteTask(string task)
    {
        if (task == "GetDocument")
        {
            MarkTaskComplete(task);
            missionManager.CheckAllMission();
            //UpdateCurrentTask(task);
        }

        if (task == "GetKey")
        {
            MarkTaskComplete(task);
            missionManager.CheckAllMission();
            //UpdateCurrentTask(task);
        }

        if (task == "GetRegen")
        {
            MarkTaskComplete(task);
            missionManager.CheckAllMission();
            //UpdateCurrentTask(task);
        }
        if (task == "GetSecretBook")
        {
            MarkTaskComplete(task);
            missionManager.CheckAllMission();
            //UpdateCurrentTask(task);
        }
    }
       

    public override bool IsDayComplete(string currentTask)
    {
        //gameState에 존재하는 키값으로 파악
        return gameState.ContainsKey("GetDocument") && gameState["GetDocument"] &&
            gameState.ContainsKey("GetKey") && gameState["GetKey"] &&
            gameState.ContainsKey("GetRegen") && gameState["GetRegen"] &&
            gameState.ContainsKey("GetSecretBook") && gameState["GetSecretBook"];
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
