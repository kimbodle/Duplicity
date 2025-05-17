using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day4Controller : DayController
{
    public Dialog[] dialog;
    bool isFirst = false;
    //다시 연구소
    public override void Initialize(string currentTask)
    {
        if (isFirst == false)
        {
            allRabbitCount = 2;
            talkRabbitCount = 0;

            //맵 아이콘 띄워주기
            UIManager.Instance.ActiveMapIcon();
            MapManager.Instance.InitializeMapRegions();

            MapManager.Instance.UnlockRegion("ShelterScene");
            MapManager.Instance.UnlockRegion("LaboratoryScene");
        }
    }

    public override void CompleteTask(string task)
    {
        if (allRabbitCount == talkRabbitCount)
        {
            //중간 저장x
            if (task == "TallWithAllRabbit")
            {
                MarkTaskComplete(task);
                DialogManager.Instance.PlayerMessageDialog(dialog[0]);
            }
        }
        if (task == "SecretLabOpen")
        {
            MarkTaskComplete(task);
            UpdateCurrentTask(task);
        }
        
    }

    public override bool IsDayComplete(string currentTask)
    {
        //gameState에 존재하는 키값으로 파악
        return gameState.ContainsKey("SecretLabOpen") && gameState["SecretLabOpen"];
    }

    public override void MapIconClick(string regionName)
    {
        if (regionName == "LibraryScene")
        {
            DialogManager.Instance.AdviseMessageDialog(1);
        }
        if (regionName == "ShelterScene")
        {
            if (IsDayComplete(GameManager.Instance.currentTask))
            {
                //NextDay(Day5) & 꿈속으로 이동
                GameManager.Instance.CompleteTask("DreamScene");
            }
            else
            {
                DialogManager.Instance.AdviseMessageDialog(1);
            }
        }
        if (regionName == "LaboratoryScene")
        {
            if (HasTalkWithAllRabbit()) //talkRabbitCount < allRabbitCount
            {
                if (SceneManager.GetActiveScene().name == regionName)
                {
                    //SceneManager.LoadScene("LibraryScene");
                }
                else
                {
                    StateManager.Instance.LoadSubScene(regionName);
                }
            }
            else
            {
                DialogManager.Instance.AdviseMessageDialog(0);
            }
        }
    }
    //task가 gameState에 있는지 확인
    private bool HasTalkWithAllRabbit()
    {
        return gameState.ContainsKey("TallWithAllRabbit") && gameState["TallWithAllRabbit"];
    }
}
