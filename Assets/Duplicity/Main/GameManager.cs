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
        //싱글톤 인스턴스에서 가져오기
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

    //Day의 모든 Task를 완료 했을 시 따로 호출
    public void CompleteTask(string SceneName)
    {
        //Day의 Task를 완료했을시
        if (currentDayController != null && currentDayController.IsDayComplete(currentTask))
        {
            Debug.Log($"{currentDay} : 완료");
            currentDay++;
            LoadNextDay(SceneName);
            currentDayController = GetCurrentDayController();
        }
        else
        {
            Debug.LogWarning("현재 Day의 모든 CompleteTask를 완료 하지 않음");
        }
    }

    public void SaveGame()
    {
        if (authController != null && !string.IsNullOrEmpty(authController.uid))
        {
            var currentState = currentDayController?.GetGameState() ?? new Dictionary<string, bool>();
            firestoreController.SaveGameState(currentDay, currentScene, currentTask, currentState);
        }
    }

    //로그인시 실행
    public void InitializeGameState(int day, string SceneName, string task, Dictionary<string, bool> loadedGameState)
    {
        isInitializingGameState = true;

        currentDay = day;
        currentTask = task;
        currentScene = SceneName;
        gameState = loadedGameState;

        UIManager.Instance.DisplayDayIntro(currentDay);
        SceneManager.LoadScene(SceneName);

        // StateManager에서 DayController 활성화
        stateManager.ActivateDayController(currentDay);
        currentDayController = GetCurrentDayController();
        currentDayController.SetGameState(gameState);
    }


    //로그인시 실행
    public void LoadGame()
    {
        if (authController != null && !string.IsNullOrEmpty(authController.uid))
        {
            //Debug.Log("LoadGame 안 if");
            firestoreController.LoadGameState((day, SceneName,task, loadedGameState) =>
            {
                InitializeGameState(day, SceneName, task, loadedGameState);
            });
        }
    }

    //씬 전환시 실행
    private void LoadDayController(int currentDay)
    {
        // StateManager에서 DayController 활성화
        stateManager.ActivateDayController(currentDay);
        stateManager.UpdateStateCurrentTask(currentTask);
    }

    private void LoadNextDay(string SceneName)
    {
        currentScene = SceneName;
        currentTask = "Intro";
        gameState.Clear();

        SaveGame();
        UIManager.Instance.DisplayDayIntro(currentDay);
        SceneManager.LoadScene(currentScene);
    }

    //Onclick 이벤트로 연결
    public void GameOver()
    {
        //EndingManager.Instance.CloseRetryUI();
        gameState.Clear();

        currentTask = "Start";

        // TaskHandler의 상태 초기화
        currentDayController?.GetTaskHandler()?.ResetTasks();

        SaveGame();
        InitializeGameState(currentDay, currentScene, currentTask, gameState);
    }

    public DayController GetCurrentDayController()
    {
        // DayController를 배열로 관리하도록 수정
        return stateManager.dayControllers[currentDay];
    }
    public int GetCurrentDay()
    {
        return currentDay;
    }
}
