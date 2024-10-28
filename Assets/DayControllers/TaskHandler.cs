using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskHandler : MonoBehaviour
{
    protected Dictionary<string, IInteractable> taskMappings;

    // �� Day�� TaskHandler���� ��ȣ�ۿ� ������ ��ü���� ����
    protected abstract void InitializeTaskMappings();

    private void OnEnable()
    {
        // Task ������ �� Day�� TaskHandler���� �ʱ�ȭ
        InitializeTaskMappings();
    }

    public void HandleTask(string taskKey)
    {
        if (taskMappings.TryGetValue(taskKey, out var interactable))
        {
            interactable.HandleTask(taskKey);
            Debug.LogWarning($"{taskKey} �߰�.");
        }
        else
        {
            Debug.LogWarning($"{taskKey}�� �������� �ʴ� �۾��Դϴ�.");
        }
    }
    public void ResetTasks()
    {
        foreach (var interactable in taskMappings.Values)
        {
            interactable.ResetTask(); // ��� IInteractable ��ü�� ResetTask ȣ��
        }
    }
}
