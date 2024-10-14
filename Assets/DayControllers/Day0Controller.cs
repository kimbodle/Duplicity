using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day0Controller : DayController
{
    private void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public override void Initialize(string currentTask)
    {
        //Day0 => Intro Initialize ���ʿ�
        Debug.Log("Day0 ����");
        //throw new System.NotImplementedException();
    }
    public override void CompleteTask(string task)
    {
        // ����
        if (task == "Intro")
        {
            MarkTaskComplete(task);
        }
        // Task �Ϸ� �� currentTask ������Ʈ
        UpdateCurrentTask(task);
    }

    public override bool IsDayComplete(string currentTask)
    {
        // ��� Task�� �Ϸ�Ǿ����� Ȯ��
        return currentTask == "Intro";
    }

}
