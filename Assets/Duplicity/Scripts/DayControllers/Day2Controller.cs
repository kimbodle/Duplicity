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
            return;
        }
        MapManager.Instance.InitializeMapRegions();
        // Task 진행 상황 로드
        LoadProgress(currentTask);

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
                MapManager.Instance.UnlockRegion("ShelterScene");
                break;

            default:
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
        if(regionName == "ShelterScene") 
        {
            GameManager.Instance.CompleteTask("ShelterScene");
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
