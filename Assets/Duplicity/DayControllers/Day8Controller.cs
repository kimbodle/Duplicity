using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day8Controller : DayController
{
    public Dialog[] dialog;
    bool isFirst = false;

    public override void Initialize(string currentTask)
    {
        //�� ������ ����ֱ�
        UIManager.Instance.ActiveMapIcon();
        MapManager.Instance.InitializeMapRegions();
        if (isFirst == false)
        {
            Debug.Log("Day8 ����");
            allRabbitCount = 3;
            talkRabbitCount = 0;

            MapManager.Instance.UnlockRegion("ShelterScene");
            MapManager.Instance.UnlockRegion("SeaScene");
            //��Ʈ���� ȹ�� ���� ���?
            MapManager.Instance.UnlockRegion("LaboratoryScnen");

            isFirst = true;
        }
    }



    public override void CompleteTask(string task)
    {
        if (allRabbitCount == talkRabbitCount)
        {
            if (task == "TallWithAllRabbit")
            {
                MarkTaskComplete(task);
                //DialogManager.Instance.PlayerMessageDialog(dialog[0]);
            }
        }
        
    }

    
    public override bool IsDayComplete(string currentTask)
    {
        throw new System.NotImplementedException();
    }

    public override void MapIconClick(string regionName)
    {
        if (SceneManager.GetActiveScene().name != regionName)
        {
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
            if (regionName == "LaboratoryScnen")
            {
                //��� �ǳ����� ��ȭ������ + ��Ʈ���� ȹ�� ���� ��� ���� �߰�
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
