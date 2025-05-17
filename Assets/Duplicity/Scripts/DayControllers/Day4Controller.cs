using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day4Controller : DayController
{
    public Dialog[] dialog;
    bool isFirst = false;
    //�ٽ� ������
    public override void Initialize(string currentTask)
    {
        if (isFirst == false)
        {
            allRabbitCount = 2;
            talkRabbitCount = 0;

            //�� ������ ����ֱ�
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
        if (regionName == "LibraryScene")
        {
            DialogManager.Instance.AdviseMessageDialog(1);
        }
        if (regionName == "ShelterScene")
        {
            if (IsDayComplete(GameManager.Instance.currentTask))
            {
                //NextDay(Day5) & �޼����� �̵�
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
    //task�� gameState�� �ִ��� Ȯ��
    private bool HasTalkWithAllRabbit()
    {
        return gameState.ContainsKey("TallWithAllRabbit") && gameState["TallWithAllRabbit"];
    }
}
