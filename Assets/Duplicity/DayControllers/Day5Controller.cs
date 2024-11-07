using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day5Controller : DayController
{
    public Dialog[] dialog;
    //bool isFirst = false;
    //�ٽ� ������
    public override void Initialize(string currentTask)
    {
        //Day5 �ǳ��� ��ȭ x
        UIManager.Instance.ActiveInventory();
        

        //�� ������ ����ֱ�
        UIManager.Instance.ActiveMapIcon();
        MapManager.Instance.InitializeMapRegions();
    }

    public override void CompleteTask(string task)
    {
        //���� ���� x
        if (allRabbitCount == talkRabbitCount)
        {
            //�߰� ����x
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
        //gameState�� �����ϴ� Ű������ �ľ�
        return gameState.ContainsKey("SecretLabOpen") && gameState["SecretLabOpen"];
    }

    public override void MapIconClick(string regionName)
    {
        
    }
    //task�� gameState�� �ִ��� Ȯ��
    private bool HasTalkWithAllRabbit()
    {
        return gameState.ContainsKey("TallWithAllRabbit") && gameState["TallWithAllRabbit"];
    }
}
