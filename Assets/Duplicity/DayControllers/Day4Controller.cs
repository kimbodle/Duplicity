using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day4Controller : DayController
{
    //�ٽ� ������
    public override void Initialize(string currentTask)
    {
        //Day3 �ļ� ó�� �ʿ� ����
        Debug.Log("Day3 ����");
        allRabbitCount = 2;
        talkRabbitCount = 0;

        //�� ������ ����ֱ�
        UIManager.Instance.OpenMapIcon();

        

        //�ļ� ó��
        if (HasTalkWithAllRabbit())
        {
            UIManager.Instance.OpenMapIcon();
            MapManager.Instance.UnlockRegion("LibraryScene");
            MapManager.Instance.UnlockRegion("ShelterScene");
            MapManager.Instance.UnlockRegion("LaboratoryScene");
        }
        else
        {
            MapManager.Instance.LockRegion("LibraryScene");
            //�����ϴ� ��
            MapManager.Instance.LockRegion("ShelterScene");
            MapManager.Instance.LockRegion("LaboratoryScene");
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
                MapManager.Instance.UnlockRegion("LaboratoryScene");
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
        if (regionName == "LaboratoryScene")
        {
            //���� ��� task �� �ȳ�������
            if (IsDayComplete(GameManager.Instance.currentTask))
            {
                DialogManager.Instance.AdviseMessageDialog(1);
                //������ �̵�
                //StateManager.Instance.LoadSubScene(regionName);
            }
            else
            {
                
            }
        }
    }
    //task�� gameState�� �ִ��� Ȯ��
    private bool HasTalkWithAllRabbit()
    {
        return gameState.ContainsKey("TallWithAllRabbit") && gameState["TallWithAllRabbit"];
    }
}
