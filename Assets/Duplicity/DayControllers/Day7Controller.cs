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
        //�ϴ� �ļ� ó��x
        Debug.Log("Day7 ����");

        UIManager.Instance.ActiveInventory();
        UIManager.Instance.ActiveMapIcon();
        //������ ���׷� start�� �ӽ�Ȱ��ȭ
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
        //gameState�� �����ϴ� Ű������ �ľ�
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
                //��� task �� �ȳ�������
                if (IsDayComplete(GameManager.Instance.currentTask))
                {
                    //NextDay(Day8) & �ǳ�ó�� �̵�
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
            Debug.Log("���� �ִ� ��");
        }
    }
}
