using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day2Controller : DayController
{
    public override void Initialize(string currentTask)
    {
        //초기화 불필요
        Debug.Log("Day2 시작");
    }

    public override void CompleteTask(string task)
    {
        if (task == "FindItem")
        {
            MarkTaskComplete(task);
        }
        // Task 완료 후 currentTask 업데이트
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
