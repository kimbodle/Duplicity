using System.Collections.Generic;
using UnityEngine;

public abstract class TaskHandler : MonoBehaviour
{
    protected Dictionary<string, IInteractable> taskMappings;

    // 각 Day의 TaskHandler에서 상호작용 가능한 객체들을 매핑합니다.
    protected abstract void InitializeTaskMappings();

    private void OnEnable()
    {
        // Task 매핑을 각 Day의 TaskHandler에서 초기화합니다.
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
            Debug.LogWarning($"{taskKey}는 지원되지 않는 작업입니다.");
        }
    }
}
