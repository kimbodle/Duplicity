using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirestoreController : MonoBehaviour
{
    public static FirestoreController Instance { get; private set; } // 싱글톤 인스턴스

    private FirebaseFirestore db;
    private FirebaseAuthController authController;

    void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 중복 인스턴스가 생성되면 파괴
        }
    }

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        authController = GetComponent<FirebaseAuthController>();
    }

    public void SaveGameState(int currentDay, string currentScene, string currentTask, Dictionary<string, bool> gameState)
    {
        if (!authController.IsLoggedIn())
        {
            Debug.LogError("로그인된 사용자가 없습니다.");
            return;
        }
        string userId = authController.User.UserId;
        DocumentReference docRef = db.Collection("users").Document(userId);
        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "currentDay", currentDay },
            { "currentSceneName", currentScene },
            { "currentTask", currentTask },
            { "gameState", gameState }
        };

        docRef.SetAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("게임 상태 저장 완료");
            }
            else
            {
                Debug.LogError("게임 상태 저장 실패: " + task.Exception);
            }
        });
    }

    public void LoadGameState(System.Action<int, string, string, Dictionary<string, bool>> onGameStateLoaded)
    {
        if (!authController.IsLoggedIn())
        {
            Debug.LogError("로그인된 사용자가 없습니다.");
            return;
        }
        string userId = authController.User.UserId;
        DocumentReference docRef = db.Collection("users").Document(userId);

        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    int currentDay = snapshot.GetValue<int>("currentDay");
                    string currentSceneName = snapshot.GetValue<string>("currentSceneName");
                    string currentTask = snapshot.GetValue<string>("currentTask");
                    Dictionary<string, bool> gameState = snapshot.GetValue<Dictionary<string, bool>>("gameState");
                    onGameStateLoaded(currentDay, currentSceneName, currentTask, gameState);
                    Debug.Log("저장된 게임 상태 불러오기. 성공");
                    Debug.Log("LoadGameState -> 현재 Day: " + currentDay + ", 현재 Task: " + currentTask);
                }
                else
                {
                    Debug.Log("저장된 게임 상태가 없습니다.");
                    onGameStateLoaded(0,"Day0Scene", "Start", new Dictionary<string, bool>()); // 기본값 설정
                }
            }
            else
            {
                Debug.LogError("게임 상태 불러오기 실패: " + task.Exception);
                onGameStateLoaded(0, "Day0Scene", "Start", new Dictionary<string, bool>()); // 오류 시 기본값 설정
            }
        });
    }
}
