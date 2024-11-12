using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day7Controller : DayController
{
    public override void Initialize(string currentTask)
    {
        //�ϴ� �ļ� ó��x
        Debug.Log("Day7 ����");
    }

    public override void CompleteTask(string task)
    {
        if (task == "day7Complete")
        {
            MarkTaskComplete(task);
            UpdateCurrentTask(task);
        }
    }
       

    public override bool IsDayComplete(string currentTask)
    {
        //gameState�� �����ϴ� Ű������ �ľ�
        return gameState.ContainsKey("day7Complete") && gameState["day7Complete"];
    }

    public override void MapIconClick(string regionName)
    {
        if (SceneManager.GetActiveScene().name != regionName)
        {
            if (regionName == "LibraryScene" || regionName == "LaboratoryScene")
            {
                DialogManager.Instance.AdviseMessageDialog(1);
            }
            if (regionName == "ShelterScene")
            {
                //��� task �� �ȳ�������
                if (IsDayComplete(GameManager.Instance.currentTask))
                {
                    //NextDay(Day8) & �ǳ�ó�� �̵�
                    GameManager.Instance.CompleteTask("ShelterScene");
                }
                else
                {
                    DialogManager.Instance.AdviseMessageDialog(1);
                }
            }
        }
        else
        {
            Debug.Log("���� �ִ� ��");
        }
    }
}
