using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day1TaskHandler : TaskHandler
{
    protected override void InitializeTaskMappings()
    {
        taskMappings = new Dictionary<string, IInteractable>();

        /*
        // FindObjectOfType를 사용할 때 해당 객체가 존재하지 않을 수 있으니 예외처리
        var newspaper = FindObjectOfType<Newspaper>();
        var map = FindObjectOfType<Map>();

        if (newspaper != null)
        {
            taskMappings.Add("FindItem", newspaper);
        }
        else
        {
            Debug.Log("Newspaper 객체를 찾을 수 없습니다.");
        }

        if (map != null)
        {
            taskMappings.Add("MapClick", map);
        }
        else
        {
            Debug.Log("Map 객체를 찾을 수 없습니다.");
        }

        // 다른 Task 매핑 추가 가능
        */
    }
}

