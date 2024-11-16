using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day8Controller : DayController
{
    public override void Initialize(string currentTask)
    {
        Debug.Log("Day8 ����");

        allRabbitCount = 3;
        talkRabbitCount = 0;

        //�� ������ ����ֱ�
        UIManager.Instance.ActiveMapIcon();
        MapManager.Instance.InitializeMapRegions();

        MapManager.Instance.UnlockAllRegion();
        //����Ʈ ���� 2 �� ȹ�� ���� ��� ������ ���� ���� �߰�
        MapManager.Instance.UnlockRegion("LaboratoryScnen");
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
                //��� �ǳ����� ��ȭ������
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
                //��� �ǳ����� ��ȭ������ + ��Ʈ���� ȹ�� ���� ��� ���� �߰� > ȸ�� ó���� ����
                if (HasTalkWithAllRabbit())
                {
                    StateManager.Instance.LoadSubScene(regionName);
                }
                else
                {
                    DialogManager.Instance.AdviseMessageDialog(1);
                }
            }
            if (regionName == "ShelterScene")
            {
                //���� �̵��� ���� �ƴ�
                DialogManager.Instance.AdviseMessageDialog(1); 
            }
        }
        else
        {
            Debug.Log("���� �ִ� ��");
        }
    }
    private bool HasTalkWithAllRabbit()
    {
        return gameState.ContainsKey("TallWithAllRabbit") && gameState["TallWithAllRabbit"];
    }
}
