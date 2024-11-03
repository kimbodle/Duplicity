using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day3Controller : DayController
{
    public override void Initialize(string currentTask)
    {
        //Day3 후속 처리 필요 없음
        Debug.Log("Day3 시작");
        allRabbitCount = 2;
        talkRabbitCount = 0;

        //맵 아이콘 띄워주기
        UIManager.Instance.OpenMapIcon();
        MapManager.Instance.InitializeMapRegions();

        MapManager.Instance.UnlockRegion("LibraryScene");
        MapManager.Instance.UnlockRegion("ShelterScene");
    }

    public override void CompleteTask(string task)
    {
        if (allRabbitCount == talkRabbitCount)
        {
            //중간 저장x
            if (task == "TallWithAllRabbit")
            {
                MarkTaskComplete(task);
            }
        }
        if (task == "Day3ComputerUnlock")
        {
            MarkTaskComplete(task);
            UpdateCurrentTask(task);
        }
        
    }

    public override bool IsDayComplete(string currentTask)
    {
        //gameState에 존재하는 키값으로 파악
        return gameState.ContainsKey("Day3ComputerUnlock") && gameState["Day3ComputerUnlock"];
    }

    public override void MapIconClick(string regionName)
    {
        if (SceneManager.GetActiveScene().name != regionName)
        {
            if (regionName == "LibraryScene")
            {
                //모든 피난묘와 대화했으면
                if (HasTalkWithAllRabbit()) //talkRabbitCount < allRabbitCount
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
                //아직 모든 task 를 안끝냈으면
                if (IsDayComplete(GameManager.Instance.currentTask))
                {
                    //NextDay(Day4) & 피난처로 이동
                    GameManager.Instance.CompleteTask("ShelterScene");
                }
                else
                {
                    DialogManager.Instance.AdviseMessageDialog(1);
                }
            }
            if (regionName == "LaboratoryScene")
            {
                DialogManager.Instance.AdviseMessageDialog(1);
                //지역 이동할 곳이 아님
                if (IsDayComplete(GameManager.Instance.currentTask))
                {
                    DialogManager.Instance.AdviseMessageDialog(1);
                    //연구실 이동
                    //StateManager.Instance.LoadSubScene(regionName);
                }
                else
                {
                    //연구실로 이동
                }
            }
        }
        else
        {
            Debug.Log("지금 있는 곳");
        }
    }
    //task가 gameState에 있는지 확인
    private bool HasTalkWithAllRabbit()
    {
        return gameState.ContainsKey("TallWithAllRabbit") && gameState["TallWithAllRabbit"];
    }
}
