using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day2Controller : DayController
{
    public override void Initialize(string currentTask)
    {
        //�ʱ�ȭ ���ʿ�
        Debug.Log("Day2 ����");
    }

    public override void CompleteTask(string task)
    {
        if (task == "FindItem")
        {
            MarkTaskComplete(task);
        }
        // Task �Ϸ� �� currentTask ������Ʈ
        UpdateCurrentTask(task);
    }

    public override bool IsDayComplete(string currentTask)
    {
        return currentTask == "FindItem";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
