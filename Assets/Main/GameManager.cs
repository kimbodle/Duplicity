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
    public bool isInitializingGameState = false;
    public Dictionary<string, bool> gameState = new Dictionary<string, bool>();

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
    }

    //Day�� Task�� �Ϸ� ���� �� ���� ȣ��
    public void CompleteTask()
    {
        //Day�� Task�� �Ϸ�������
        if (currentDayController != null && currentDayController.IsDayComplete(currentTask))
        {
            Debug.Log($"{currentDay} : �Ϸ�");
            currentDay++;
            LoadNextDay();
            currentDayController = GetCurrentDayController();
        }
        else
        {
            //SaveGame(); //�� Task�� ���������� �ڵ� ����
        }
    }

    public void SaveGame()
    {
        if (authController != null && !string.IsNullOrEmpty(authController.uid))
        {
            var currentState = currentDayController?.GetGameState() ?? new Dictionary<string, bool>();
            firestoreController.SaveGameState(currentDay, currentTask, currentState);
        }
    }

    //�α��ν� ����
    public void InitializeGameState(int day, string task, Dictionary<string, bool> loadedGameState)
    {
        isInitializingGameState = true;

        currentDay = day;
        currentTask = task;
        gameState = loadedGameState;

        UIManager.Instance.DisplayDayIntro(currentDay);
        string sceneName = "Day" + currentDay + "Scene";
        SceneManager.LoadScene(sceneName);

        // StateManager���� DayController Ȱ��ȭ
        stateManager.ActivateDayController(currentDay);
        currentDayController = GetCurrentDayController();
        // ���� �ε�Ǹ� OnSceneLoaded���� LoadDayController ȣ��
    }


    //�α��ν� ����
    public void LoadGame()
    {
        if (authController != null && !string.IsNullOrEmpty(authController.uid))
        {
            //Debug.Log("LoadGame �� if");
            firestoreController.LoadGameState((day, task, loadedGameState) =>
            {
                InitializeGameState(day, task, loadedGameState);
            });
        }
    }

    //�� ��ȯ�� ����
    private void LoadDayController(int currentDay)
    {
        // StateManager���� DayController Ȱ��ȭ
        stateManager.ActivateDayController(currentDay);
        stateManager.UpdateStateCurrentTask(currentTask);
    }

    private void LoadNextDay()
    {
        string nextSceneName = "Day" + currentDay + "Scene";
        currentTask = "Intro";
        gameState.Clear();
        currentTask = "Start";
        SaveGame();
        UIManager.Instance.DisplayDayIntro(currentDay);
        SceneManager.LoadScene(nextSceneName);
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

    //Onclick �̺�Ʈ�� ����
    public void GameOver()
    {
        EndingManager.Instance.CloseRetryUI();
        gameState.Clear();

        currentTask = "Start";

        // TaskHandler�� ���� �ʱ�ȭ
        currentDayController?.GetTaskHandler()?.ResetTasks(); 

        SaveGame();
        InitializeGameState(currentDay, currentTask, gameState);
    }
}
