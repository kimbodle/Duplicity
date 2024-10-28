using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day1TaskHandler : TaskHandler
{
    protected override void InitializeTaskMappings()
    {
        taskMappings = new Dictionary<string, IInteractable>();

        
        // FindObjectOfType�� ����� �� �ش� ��ü�� �������� ���� �� ������ ����ó��
        //�ļ�ó���� �ʿ��� �͸� �ֱ�
        var newspaper = FindObjectOfType<Newspaper>();
        //var map = FindObjectOfType<Map>();

        if (newspaper != null)
        {
            taskMappings.Add("FindItem", newspaper);
        }
        else
        {
            Debug.Log("Newspaper ��ü�� ã�� �� �����ϴ�.");
        }

        //if (map != null)
        //{
        //    taskMappings.Add("MapClick", map);
        //}
        //else
        //{
        //    Debug.Log("Map ��ü�� ã�� �� �����ϴ�.");
        //}

        // �ٸ� Task ���� �߰�
        
    }

    //��� Task�� ���¸� �ʱ�ȭ�ϴ� �޼���
    //public void ResetTasks()
    //{
    //    foreach (var interactable in taskMappings.Values)
    //    {
    //        if (interactable is Newspaper newspaper)
    //        {
    //            newspaper.ResetState();
    //        }
    //        �ٸ� IInteractable ��ü�� �ʱ�ȭ �޼��尡 �ʿ��ϸ� �߰��ϱ�
    //    }
    //}
}

