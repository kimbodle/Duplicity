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

        //Map
        UIManager.Instance.ActiveMapIcon();

        //Map 초기화
        MapManager.Instance.InitializeMapRegions();
        MapManager.Instance.UnlockRegion("SeaScene");
        //프린트 파일 2 를 획득 했을 경우 연구소 오픈 로직 추가
        bool hasRecipe = GameManager.Instance.HasSeenEnding("EndingItem", 0);
        Debug.Log(hasRecipe);
        if (hasRecipe)
        {
            MapManager.Instance.UnlockRegion("LaboratoryScene");
        }
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
                //모든 피난묘와 대화했으면 + 제조법을 획득했을 경우 이동
                if (HasTalkWithAllRabbit())
                {
                    StateManager.Instance.LoadSubScene(regionName);
                }
                else
                {
                    DialogManager.Instance.AdviseMessageDialog(0);
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
