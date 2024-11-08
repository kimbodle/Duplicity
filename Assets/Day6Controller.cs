using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day6Controller : DayController
{
    public override void Initialize(string currentTask)
    {
        //Day6
        Debug.Log("Day6 ����");
        allRabbitCount = 3;
        talkRabbitCount = 0;

        //�� ������ ����ֱ�
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
                //������ ��ũ��Ʈ�� �޼ҵ� ����
                DialogManager.Instance.OnDialogEnd += ShowChoicesAfterDialog;

            }
            //�߰� ���� o
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
        //gameState�� �����ϴ� Ű������ �ľ�
        return gameState.ContainsKey("EnterRegenRepo") && gameState["EnterRegenRepo"];
    }

    public override void MapIconClick(string regionName)
    {
        if (regionName == "ShelterScene")
        {
            if (SceneManager.GetActiveScene().name == regionName)
            {
                Debug.Log("���� ��");
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
                    Debug.Log("���� ��");
                    //x
                }
                //���� ������ ��ȭ�� ������ �ʾ�����
                if (HasOpenRegen())
                {
                    StateManager.Instance.LoadSubScene(regionName);
                    //���� ���� ����� �� �ε�
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
        // ������ ��ȭ ǥ��
        FindObjectOfType<Day6DialogSelection>().ShowChoices();

        // �̺�Ʈ ���� �����Ͽ� ���� �� ������� �ʵ��� ����
        DialogManager.Instance.OnDialogEnd -= ShowChoicesAfterDialog;
    }
}
