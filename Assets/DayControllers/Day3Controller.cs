using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day3Controller : DayController
{
    public override void Initialize(string currentTask)
    {
        //Day3 �ļ� ó�� �ʿ� ����
        Debug.Log("Day3 ����");
        allRabbitCount = 2;
        talkRabbitCount = 0;

        //�� ������ ����ֱ�
        UIManager.Instance.OpenMapIcon();

        MapManager.Instance.LockRegion("LibraryScene");
        //�����ϴ� ��
        MapManager.Instance.LockRegion("ShelterScene");

        //�ļ� ó��
        if (HasTalkWithAllRabbit())
        {
            UIManager.Instance.OpenMapIcon();
            MapManager.Instance.UnlockRegion("LibraryScene");
            MapManager.Instance.UnlockRegion("ShelterScene");
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
                MapManager.Instance.UnlockRegion("LibraryScene");
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
        if (regionName == "LibraryScene")
        {
            //��� �ǳ����� ��ȭ������
            if (HasTalkWithAllRabbit()) //talkRabbitCount < allRabbitCount
            {
                if(SceneManager.GetActiveScene().name == regionName)
                {
                    Debug.Log("���� ��");
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
    }
    //task�� gameState�� �ִ��� Ȯ��
    private bool HasTalkWithAllRabbit()
    {
        return gameState.ContainsKey("TallWithAllRabbit") && gameState["TallWithAllRabbit"];
    }
}
