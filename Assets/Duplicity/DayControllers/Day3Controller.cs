using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day3Controller : DayController
{
    public Dialog[] dialog;
    bool isFirst = false;
    public override void Initialize(string currentTask)
    {
        if(isFirst == false)
        {
            //Day3 �ļ� ó�� �ʿ� ����
            Debug.Log("Day3 ����");
            allRabbitCount = 2;
            talkRabbitCount = 0;

            //�� ������ ����ֱ�
            UIManager.Instance.OpenMapIcon();
            MapManager.Instance.InitializeMapRegions();

            MapManager.Instance.UnlockRegion("LibraryScene");
            MapManager.Instance.UnlockRegion("ShelterScene");

            //Dialog
            DialogManager.Instance.PlayerMessageDialog(dialog[0]);

            isFirst = true;
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
                DialogManager.Instance.PlayerMessageDialog(dialog[1]);
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
        //gameState�� �����ϴ� Ű������ �ľ�
        return gameState.ContainsKey("Day3ComputerUnlock") && gameState["Day3ComputerUnlock"];
    }

    public override void MapIconClick(string regionName)
    {
        if (SceneManager.GetActiveScene().name != regionName)
        {
            if (regionName == "LibraryScene")
            {
                //��� �ǳ����� ��ȭ������
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
                //���� ��� task �� �ȳ�������
                if (IsDayComplete(GameManager.Instance.currentTask))
                {
                    //NextDay(Day4) & �ǳ�ó�� �̵�
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
                //���� �̵��� ���� �ƴ�
                if (IsDayComplete(GameManager.Instance.currentTask))
                {
                    DialogManager.Instance.AdviseMessageDialog(1);
                    //������ �̵�
                    //StateManager.Instance.LoadSubScene(regionName);
                }
                else
                {
                    //�����Ƿ� �̵�
                }
            }
        }
        else
        {
            Debug.Log("���� �ִ� ��");
        }
    }
    //task�� gameState�� �ִ��� Ȯ��
    private bool HasTalkWithAllRabbit()
    {
        return gameState.ContainsKey("TallWithAllRabbit") && gameState["TallWithAllRabbit"];
    }
}
