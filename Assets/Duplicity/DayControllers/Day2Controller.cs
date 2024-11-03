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
        MapManager.Instance.InitializeMapRegions();
        // GameManager���� gameState�� ������ ���
        //�� �̰� Ʋ�� �� ���� �̹� DayController�� ���� �Ǿ�����
        //gameState = gameManager.gameState;

        //taskHandler = GetComponent<TaskHandler>();

        // Task ���� ��Ȳ �ε�
        LoadProgress(currentTask);

        Debug.Log("Day2 ����");
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
                Debug.LogWarning($"�� �� ���� task: {task}");
                break;
        }

        // Task �Ϸ� �� currentTask ������Ʈ
        UpdateCurrentTask(task);
    }

    public override bool IsDayComplete(string currentTask)
    {
        return currentTask == "ItemCollected";
    }

    public override void MapIconClick(string regionName)
    {
        Debug.Log("MapClick �ƾ��");
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

    //������ ����鼭 �ʿ� ���� ��
    public void ReloadCurrentScene()
    {
        // ���� ���� �̸��� �����´�.
        string currentSceneName = SceneManager.GetActiveScene().name;

        // ���� �ٽ� �ε��Ѵ�.
        SceneManager.LoadScene(currentSceneName);
    }
}
