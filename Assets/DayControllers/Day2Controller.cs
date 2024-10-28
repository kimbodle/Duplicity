using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day2Controller : DayController
{
    private TaskHandler taskHandler;

    private void OnEnable()
    {
        taskHandler = GetComponent<TaskHandler>();
    }
    public override void Initialize(string currentTask)
    {
        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager is null");
            return;
        }

        // GameManager에서 gameState를 가져와 사용
        gameState = gameManager.gameState;
        //taskHandler = GetComponent<TaskHandler>();

        // Task 진행 상황 로드
        LoadProgress(currentTask);

        Debug.Log("Day2 시작");
    }

    public override void CompleteTask(string task)
    {
        switch (task)
        {
            case "ItemCollected":
                MarkTaskComplete("ItemCollected");
                break;

            case "FindItem":
                MarkTaskComplete("FindItem");
                break;

            default:
                Debug.LogWarning($"알 수 없는 task: {task}");
                break;
        }

        // Task 완료 후 currentTask 업데이트
        UpdateCurrentTask(task);
    }

    public override bool IsDayComplete(string currentTask)
    {
        return currentTask == "ItemCollected";
    }

    public override void MapIconClick(string regionName)
    {
        Debug.Log("MapClick 됐어요");
        if(regionName == "Shelter")
        {
            GameManager.Instance.CompleteTask();
        }
    }

    protected override void HandleTaskCompletion(string taskKey)
    {
        if (taskKey == "Start")
        {
            ReloadCurrentScene();
            return;
        }

        taskHandler.HandleTask(taskKey);
    }

    //리셋이 생기면서 필요 없을 듯
    public void ReloadCurrentScene()
    {
        // 현재 씬의 이름을 가져온다.
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 씬을 다시 로드한다.
        SceneManager.LoadScene(currentSceneName);
    }
}
