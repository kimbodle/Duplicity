using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day5Controller : DayController
{
    public Dialog[] dialog;
    //bool isFirst = false;
    //다시 연구소
    public override void Initialize(string currentTask)
    {
        //Day5 피난묘 대화 x
        UIManager.Instance.ActiveInventory();
        

        //맵 아이콘 띄워주기
        UIManager.Instance.ActiveMapIcon();
        MapManager.Instance.InitializeMapRegions();
    }

    public override void CompleteTask(string task)
    {
        //아직 수정 x
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
        
    }
    //task가 gameState에 있는지 확인
    private bool HasTalkWithAllRabbit()
    {
        return gameState.ContainsKey("TallWithAllRabbit") && gameState["TallWithAllRabbit"];
    }
}
