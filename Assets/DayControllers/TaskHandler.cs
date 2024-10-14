using System.Collections.Generic;
using UnityEngine;

public abstract class TaskHandler : MonoBehaviour
{
    protected Dictionary<string, IInteractable> taskMappings;

    // �� Day�� TaskHandler���� ��ȣ�ۿ� ������ ��ü���� �����մϴ�.
    protected abstract void InitializeTaskMappings();

    private void OnEnable()
    {
        // Task ������ �� Day�� TaskHandler���� �ʱ�ȭ�մϴ�.
        InitializeTaskMappings();
    }

    public void HandleTask(string taskKey)
    {
        if (taskMappings.TryGetValue(taskKey, out var interactable))
        {
            interactable.HandleTask(taskKey);
        }
        else
        {
            Debug.LogWarning($"{taskKey}�� �������� �ʴ� �۾��Դϴ�.");
        }
    }
}
