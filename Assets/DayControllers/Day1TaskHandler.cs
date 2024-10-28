using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day1TaskHandler : TaskHandler
{
    protected override void InitializeTaskMappings()
    {
        taskMappings = new Dictionary<string, IInteractable>();

        
        // FindObjectOfType를 사용할 때 해당 객체가 존재하지 않을 수 있으니 예외처리
        //후속처리가 필요한 것만 넣기
        var newspaper = FindObjectOfType<Newspaper>();
        //var map = FindObjectOfType<Map>();

        if (newspaper != null)
        {
            taskMappings.Add("FindItem", newspaper);
        }
        else
        {
            Debug.Log("Newspaper 객체를 찾을 수 없습니다.");
        }

        //if (map != null)
        //{
        //    taskMappings.Add("MapClick", map);
        //}
        //else
        //{
        //    Debug.Log("Map 객체를 찾을 수 없습니다.");
        //}

        // 다른 Task 매핑 추가
        
    }

    //모든 Task의 상태를 초기화하는 메서드
    //public void ResetTasks()
    //{
    //    foreach (var interactable in taskMappings.Values)
    //    {
    //        if (interactable is Newspaper newspaper)
    //        {
    //            newspaper.ResetState();
    //        }
    //        다른 IInteractable 객체에 초기화 메서드가 필요하면 추가하기
    //    }
    //}
}

