using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private int currentDay = 0;
    [SerializeField] private DayController currentDayController;

    public string currentTask = "Start";
    public string currentScene = "Day0Scene";
    public bool isInitializingGameState = false;
    public Dictionary<string, bool> gameState = new Dictionary<string, bool>();
    //����
    public Dictionary<string, bool> endingAlbum = new Dictionary<string, bool>();

    private FirestoreController firestoreController;
    private FirebaseAuthController authController;
    private StateManager stateManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //�̱��� �ν��Ͻ����� ��������
        firestoreController = FirestoreController.Instance;
        authController = FirebaseAuthController.Instance;
        stateManager = StateManager.Instance;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!isInitializingGameState)
        {
            LoadDayController(currentDay);
        }
        isInitializingGameState = false;

        if (currentDay != 0 && currentDay != 1 && currentDay != 9)
        {
            UIManager.Instance.ActiveDialogHistoryIcon();
        }
        else
        {
            UIManager.Instance.DeactivateDialogHistoryIcon();
        }
    }

    //Day�� ��� Task�� �Ϸ� ���� �� ���� ȣ��
    public void CompleteTask(string SceneName)
    {
        //Day�� Task�� �Ϸ�������
        if (currentDayController != null && currentDayController.IsDayComplete(currentTask))
        {
            Debug.Log($"{currentDay} : �Ϸ�");
            currentDay++;
            LoadNextDay(SceneName);
            currentDayController = GetCurrentDayController();
        }
        else
        {
            Debug.LogWarning("���� Day�� ��� CompleteTask�� �Ϸ� ���� ����");
        }
    }

    public void SaveGame()
    {
        if (authController != null && !string.IsNullOrEmpty(authController.uid))
        {
            var currentState = currentDayController?.GetGameState() ?? new Dictionary<string, bool>();
            firestoreController.SaveGameState(currentDay, currentScene, currentTask, currentState, endingAlbum);
        }
    }
    // ���� �ٹ� ������Ʈ �� ����
    public void SaveEnding(string endingType, int endingIndex)
    {
        string endingKey = $"{endingType}_{endingIndex}";
        if (!endingAlbum.ContainsKey(endingKey) || !endingAlbum[endingKey])
        {
            endingAlbum[endingKey] = true;
            Debug.Log(endingKey);
            SaveGame();
            //// UI�� ���� �̹��� ǥ��
            //UIManager.Instance.DisplayEndingImage(endingType, endingIndex);
        }
    }

    //�α��ν� ����
    public void LoadGame()
    {
        if (authController != null && !string.IsNullOrEmpty(authController.uid))
        {
            //Debug.Log("LoadGame �� if");
            firestoreController.LoadGameState((day, SceneName, task, loadedGameState, loadedEndingAlbum) =>
            {
                InitializeGameState(day, SceneName, task, loadedGameState, loadedEndingAlbum);
            });
        }
    }

    //�α��ν� ����
    public void InitializeGameState(int day, string SceneName, string task,
        Dictionary<string, bool> loadedGameState, Dictionary<string, bool> loadedEndingAlbum)
    {
        isInitializingGameState = true;

        currentDay = day;
        currentTask = task;
        currentScene = SceneName;
        gameState = loadedGameState;

        endingAlbum = loadedEndingAlbum;

        foreach (var ending in endingAlbum)
        {
            Debug.Log(ending);
            if (ending.Value)
            {
                string[] endingData = ending.Key.Split('_');
                string endingType = endingData[0];
                int endingIndex = int.Parse(endingData[1]);
            }
        }

        UIManager.Instance.DisplayDayIntro(currentDay);
        SceneManager.LoadScene(SceneName);

        // StateManager���� DayController Ȱ��ȭ
        stateManager.ActivateDayController(currentDay);
        currentDayController = GetCurrentDayController();
        currentDayController.SetGameState(gameState);
    }

    //�� ��ȯ�� ����
    private void LoadDayController(int currentDay)
    {
        // StateManager���� DayController Ȱ��ȭ
        stateManager.ActivateDayController(currentDay);
        stateManager.UpdateStateCurrentTask(currentTask);
    }

    private void LoadNextDay(string SceneName)
    {
        currentScene = SceneName;
        currentTask = "Intro";
        gameState.Clear();
        currentDayController?.ClearGameState();
        DialogManager.Instance.ClearHistory();

        SaveGame();
        UIManager.Instance.DisplayDayIntro(currentDay);
        SceneManager.LoadScene(currentScene);
    }

    //Onclick �̺�Ʈ�� ����
    public void GameOver()
    {
        //EndingManager.Instance.CloseRetryUI();

        currentTask = "Intro";
        gameState.Clear();
        currentDayController?.ClearGameState();

        // TaskHandler�� ���� �ʱ�ȭ
        currentDayController?.GetTaskHandler()?.ResetTasks();

        SaveGame();
        InitializeGameState(currentDay, currentScene, currentTask, gameState, endingAlbum);
    }

    public void ResetGameState()
    {
        // ���� ���� ���� �ʱ�ȭ
        currentDay = 0;
        currentTask = "Start";
        currentScene = "Day0Scene";
        gameState.Clear();

        SaveGame();

        Debug.Log("���� ���� �ʱ�ȭ �Ϸ� (endingAlbum ����)");
    }


    public bool HasSeenEnding(string endingType, int endingIndex)
    {
        string endingKey = $"{endingType}_{endingIndex}";

        // ��ųʸ����� �ش� Ű�� �����ϰ�, �� ���� true�� ��� �ش� ������ �� ������ ó��
        if (endingAlbum.ContainsKey(endingKey) && endingAlbum[endingKey])
        {
            Debug.Log($"{endingKey} ������ �̹� ��.");
            return true;
        }
        else
        {
            Debug.Log($"{endingKey} ������ ���� ���� �ʾ���.");
            return false;
        }
    }

    public DayController GetCurrentDayController()
    {
        // DayController�� �迭�� �����ϵ��� ����
        return stateManager.dayControllers[currentDay];
    }
    public int GetCurrentDay()
    {
        return currentDay;
    }
}
