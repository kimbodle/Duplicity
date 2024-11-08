using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day6Controller : DayController
{
    public override void Initialize(string currentTask)
    {
        //Day6
        Debug.Log("Day6 시작");
        allRabbitCount = 3;
        talkRabbitCount = 0;

        //맵 아이콘 띄워주기
        UIManager.Instance.ActiveMapIcon();
        MapManager.Instance.InitializeMapRegions();

        MapManager.Instance.UnlockRegion("ShelterScene");
        MapManager.Instance.UnlockRegion("RegenScene");
    }

    public override void CompleteTask(string task)
    {
        if (allRabbitCount == talkRabbitCount)
        {
            
            if (task == "TallWithAllRabbit")
            {
                MarkTaskComplete(task);
                //선택지 스크립트의 메소드 실행
                DialogManager.Instance.OnDialogEnd += ShowChoicesAfterDialog;

            }
            //중간 저장 o
            if (task == "OpenRegen")
            {
                MarkTaskComplete(task);
                UpdateCurrentTask(task);
            }

            if (task == "EnterRegenRepo")
            {
                MarkTaskComplete(task);
                UpdateCurrentTask(task);
            }
        }
    }

    public override bool IsDayComplete(string currentTask)
    {
        //gameState에 존재하는 키값으로 파악
        return gameState.ContainsKey("EnterRegenRepo") && gameState["EnterRegenRepo"];
    }

    public override void MapIconClick(string regionName)
    {
        if (regionName == "ShelterScene")
        {
            if (SceneManager.GetActiveScene().name == regionName)
            {
                Debug.Log("현재 씬");
            }
            else
            {
                //x
            }
        }
        if (regionName == "RegenScene")
        {
            if (HasTalkWithAllRabbit())
            {
                if (SceneManager.GetActiveScene().name == regionName)
                {
                    Debug.Log("현재 씬");
                    //x
                }
                //아직 선택지 대화를 끝내지 않았으면
                if (HasOpenRegen())
                {
                    StateManager.Instance.LoadSubScene(regionName);
                    //이후 리젠 저장소 씬 로드
                }
                else
                {
                    DialogManager.Instance.AdviseMessageDialog(1);
                }
            }
            else
            {
                DialogManager.Instance.AdviseMessageDialog(0);
            }
        }
    }
    private bool HasTalkWithAllRabbit()
    {
        return gameState.ContainsKey("TallWithAllRabbit") && gameState["TallWithAllRabbit"];
    }

    private bool HasOpenRegen()
    {
        return gameState.ContainsKey("OpenRegen") && gameState["OpenRegen"];
    }

    private void ShowChoicesAfterDialog()
    {
        // 선택지 대화 표시
        FindObjectOfType<Day6DialogSelection>().ShowChoices();

        // 이벤트 구독 해제하여 여러 번 실행되지 않도록 설정
        DialogManager.Instance.OnDialogEnd -= ShowChoicesAfterDialog;
    }
}
