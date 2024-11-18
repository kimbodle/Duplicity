using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }

    private GameManager gameManager;

    [SerializeField] private string currentTask;

    public DayController[] dayControllers; // DayController를 배열로 관리

    private DayController currentDayController;

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
        gameManager = FindObjectOfType<GameManager>();

        // DayController들을 미리 비활성화
        foreach (var controller in dayControllers)
        {
            if (controller != null)
            {
                controller.gameObject.SetActive(false);
            }
        }
    }

    // Day에 따른 DayController 활성화
    public void ActivateDayController(int currentDay)
    {
        DeactivateAllDayControllers();

        if (currentDay >= 0 && currentDay <= dayControllers.Length)
        {
            var controller = dayControllers[currentDay];

            if (controller != null)
            {
                currentDayController = controller;
                controller.gameObject.SetActive(true);
                if (InventoryManager.Instance != null)
                {
                    InventoryManager.Instance.ClearAllItemSlot();
                }
                controller.Initialize(currentTask);
            }
        }
    }
    public void UpdateGameState()
    {
        currentDayController.Initialize(currentTask);
    }

    // 모든 DayController 비활성화
    private void DeactivateAllDayControllers()
    {
        foreach (var controller in dayControllers)
        {
            if (controller != null)
            {
                controller.gameObject.SetActive(false);
            }
        }
    }

    // 씬 이동 및 상태 저장 처리
    public void LoadSubScene(string sceneName)
    {
        SaveCurrentState();
        SceneManager.LoadScene(sceneName);
    }

    public void SaveCurrentState()
    {
        if (gameManager != null)
        {
            gameManager.SaveGame();  // 현재 상태를 저장
        }
    }

    public void LoadStateFromFirestore()
    {
        if (gameManager != null)
        {
            gameManager.LoadGame();  // Firestore에서 상태를 불러옴
        }
    }

    public void UpdateStateCurrentTask(string task)
    {
        currentTask = task;
    }

    public DayController GetCurrentDayController()
    {
        return currentDayController;
    }
}
