using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class DayController : MonoBehaviour
{
    protected Dictionary<string, bool> gameState = new Dictionary<string, bool>();
    protected GameManager gameManager;
    public int allRabbitCount = 0;
    public int talkRabbitCount = 0;
    public abstract void Initialize(string currentTask);
    public abstract void CompleteTask(string task);
    public abstract bool IsDayComplete(string currentTask);

    public abstract void MapIconClick(string regionName);

    public TaskHandler GetTaskHandler()
    {
        //Debug.Log("GetTaskHandler");
        TaskHandler taskHandler = GetComponent<TaskHandler>();
        if (taskHandler == null)
        {
            return null;
        }
        return taskHandler;
    }
    public Dictionary<string, bool> GetGameState()
    {
        return new Dictionary<string, bool>(gameState);
    }

    public void SetGameState(Dictionary<string, bool> state)
    {
        gameState = new Dictionary<string, bool>(state);
        Debug.Log("gameState 초기화");
        foreach (var taskKey in gameState.Keys)
        {
            if (gameState[taskKey])
            {
                Debug.Log($"gameState : {taskKey} = true");
            }

        }
    }
    public void ClearGameState()
    {
        gameState.Clear();
        gameState["Intro"] = true;
        Debug.Log("gameState가 비워짐.");
    }

    // Task 진행 상황 불러오기
    protected void LoadProgress(string currentTask)
    {
        Debug.Log($"LoadProgress: currentTask = {currentTask}");

        // 완료된 작업들을 저장할 리스트
        List<string> completedTasks = new List<string>();

        // gameState를 순회하며 완료된 작업 확인
        foreach (var taskKey in gameState.Keys)
        {
            if (gameState[taskKey])
            {
                Debug.Log($"{taskKey} task가 완료됨.");
                completedTasks.Add(taskKey);  // 완료된 작업을 리스트에 추가
            }
            else if (taskKey == currentTask)
            {
                Debug.Log($"현재 작업은 {currentTask}, 작업 진행 중...");
                completedTasks.Add(currentTask);  // 현재 진행 중인 작업 추가
            }
        }

        // 완료된 작업들 처리
        foreach (var taskKey in completedTasks)
        {
            HandleTaskCompletion(taskKey);
        }
    }

    // Task별 후속 작업 처리
    protected virtual void HandleTaskCompletion(string taskKey)
    {
        Debug.Log($"{taskKey}의 후속 작업 처리");
    }

    protected void MarkTaskComplete(string task)
    {
        if (!gameState.ContainsKey(task))
        {
            gameState[task] = true;
            Debug.Log($"{task} 완료");
        }
        else
        {
            Debug.LogWarning($"이미 완료한 {task}. gameState에 이미 존재");
        }
    }

    protected void UpdateCurrentTask(string task)
    {
        GameManager.Instance.currentTask = task;
        GameManager.Instance.currentScene = SceneManager.GetActiveScene().name;

        Debug.Log($"currentTask 업데이트: {task}");
        GameManager.Instance.SaveGame();
    }
}
