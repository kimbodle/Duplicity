using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day1Controller : DayController
{
    private TaskHandler taskHandler;

    private void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
        taskHandler = GetComponent<TaskHandler>();
    }

    public override void Initialize(string task)
    {
        if (gameManager == null)
        {
            Debug.LogError("GameManager is null");
            return;
        }

        // GameManager에서 gameState를 가져와 사용
        gameState = gameManager.gameState;

        // Task 진행 상황 로드
        LoadProgress(task);
    }

    public override void CompleteTask(string task)
    {
        Debug.Log($"{task} 작업이 수행됨.");

        // Task 완료 시 로직 처리
        switch (task)
        {
            case "FindItem":
                MarkTaskComplete("FindItem");
                break;

            case "MapClick":
                MarkTaskComplete("MapClick");
                break;

            case "Moveshelter":
                MarkTaskComplete("Moveshelter");
                break;

            default:
                Debug.LogWarning($"Unknown task: {task}");
                break;
        }

        // Task 완료 후 currentTask 업데이트
        UpdateCurrentTask(task);
    }

    public override bool IsDayComplete(string task)
    {
        // 모든 Task가 완료되었는지 확인
        return task == "Moveshelter";
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

    public void ReloadCurrentScene()
    {
        // 현재 씬의 이름을 가져온다.
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 씬을 다시 로드한다.
        SceneManager.LoadScene(currentSceneName);
    }
}
