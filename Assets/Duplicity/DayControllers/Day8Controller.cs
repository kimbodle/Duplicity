using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day8Controller : DayController
{
    public override void Initialize(string currentTask)
    {
        Debug.Log("Day8 시작");

        allRabbitCount = 3;
        talkRabbitCount = 0;

        //맵 아이콘 띄워주기
        UIManager.Instance.ActiveMapIcon();
        MapManager.Instance.InitializeMapRegions();

        MapManager.Instance.UnlockAllRegion();
        //노트북을 획득 했을 경우?
        MapManager.Instance.UnlockRegion("LaboratoryScnen");
    }



    public override void CompleteTask(string task)
    {
        if (allRabbitCount == talkRabbitCount)
        {
            if (task == "TallWithAllRabbit")
            {
                MarkTaskComplete(task);
            }
        }
        
    }

    
    public override bool IsDayComplete(string currentTask)
    {
        return gameState.ContainsKey("GetDocument") && gameState["GetDocument"] &&
           gameState.ContainsKey("GetKey") && gameState["GetKey"] &&
           gameState.ContainsKey("GetRegen") && gameState["GetRegen"] &&
           gameState.ContainsKey("GetSecretBook") && gameState["GetSecretBook"];
    }

    public override void MapIconClick(string regionName)
    {
        if (SceneManager.GetActiveScene().name != regionName)
        {
            if (regionName == "LibraryScene" || regionName == "RegenScene")
            {
                DialogManager.Instance.AdviseMessageDialog(1);
            }

            if (regionName == "SeaScene")
            {
                //모든 피난묘와 대화했으면
                if (HasTalkWithAllRabbit())
                {
                    StateManager.Instance.LoadSubScene(regionName);
                }
                else
                {
                    DialogManager.Instance.AdviseMessageDialog(0);
                }
            }
            if (regionName == "LaboratoryScene")
            {
                //모든 피난묘와 대화했으면 + 노트북을 획득 했을 경우 조건 추가
                if (HasTalkWithAllRabbit())
                {
                    StateManager.Instance.LoadSubScene(regionName);
                }
                else
                {
                    DialogManager.Instance.AdviseMessageDialog(1);
                }
            }
            if (regionName == "ShelterScene")
            {
                //지역 이동할 곳이 아님
                DialogManager.Instance.AdviseMessageDialog(1); 
            }
        }
        else
        {
            Debug.Log("지금 있는 곳");
        }
    }
    private bool HasTalkWithAllRabbit()
    {
        return gameState.ContainsKey("TallWithAllRabbit") && gameState["TallWithAllRabbit"];
    }
}
