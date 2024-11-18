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
        Debug.Log("gameState �ʱ�ȭ");
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
        Debug.Log("gameState�� �����.");
    }

    // Task ���� ��Ȳ �ҷ�����
    protected void LoadProgress(string currentTask)
    {
        Debug.Log($"LoadProgress: currentTask = {currentTask}");

        // �Ϸ�� �۾����� ������ ����Ʈ
        List<string> completedTasks = new List<string>();

        // gameState�� ��ȸ�ϸ� �Ϸ�� �۾� Ȯ��
        foreach (var taskKey in gameState.Keys)
        {
            if (gameState[taskKey])
            {
                Debug.Log($"{taskKey} task�� �Ϸ��.");
                completedTasks.Add(taskKey);  // �Ϸ�� �۾��� ����Ʈ�� �߰�
            }
            else if (taskKey == currentTask)
            {
                Debug.Log($"���� �۾��� {currentTask}, �۾� ���� ��...");
                completedTasks.Add(currentTask);  // ���� ���� ���� �۾� �߰�
            }
        }

        // �Ϸ�� �۾��� ó��
        foreach (var taskKey in completedTasks)
        {
            HandleTaskCompletion(taskKey);
        }
    }

    // Task�� �ļ� �۾� ó��
    protected virtual void HandleTaskCompletion(string taskKey)
    {
        Debug.Log($"{taskKey}�� �ļ� �۾� ó��");
    }

    protected void MarkTaskComplete(string task)
    {
        if (!gameState.ContainsKey(task))
        {
            gameState[task] = true;
            Debug.Log($"{task} �Ϸ�");
        }
        else
        {
            Debug.LogWarning($"�̹� �Ϸ��� {task}. gameState�� �̹� ����");
        }
    }

    protected void UpdateCurrentTask(string task)
    {
        GameManager.Instance.currentTask = task;
        GameManager.Instance.currentScene = SceneManager.GetActiveScene().name;

        Debug.Log($"currentTask ������Ʈ: {task}");
        GameManager.Instance.SaveGame();
    }
}
