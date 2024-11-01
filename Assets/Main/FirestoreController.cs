using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirestoreController : MonoBehaviour
{
    public static FirestoreController Instance { get; private set; } // �̱��� �ν��Ͻ�

    private FirebaseFirestore db;
    private FirebaseAuthController authController;

    void Awake()
    {
        // �̱��� ���� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ� �ν��Ͻ��� �����Ǹ� �ı�
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
            Debug.LogError("�α��ε� ����ڰ� �����ϴ�.");
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
                Debug.Log("���� ���� ���� �Ϸ�");
            }
            else
            {
                Debug.LogError("���� ���� ���� ����: " + task.Exception);
            }
        });
    }

    public void LoadGameState(System.Action<int, string, string, Dictionary<string, bool>> onGameStateLoaded)
    {
        if (!authController.IsLoggedIn())
        {
            Debug.LogError("�α��ε� ����ڰ� �����ϴ�.");
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
                    Debug.Log("����� ���� ���� �ҷ�����. ����");
                    Debug.Log("LoadGameState -> ���� Day: " + currentDay + ", ���� Task: " + currentTask);
                }
                else
                {
                    Debug.Log("����� ���� ���°� �����ϴ�.");
                    onGameStateLoaded(0,"Day0Scene", "Start", new Dictionary<string, bool>()); // �⺻�� ����
                }
            }
            else
            {
                Debug.LogError("���� ���� �ҷ����� ����: " + task.Exception);
                onGameStateLoaded(0, "Day0Scene", "Start", new Dictionary<string, bool>()); // ���� �� �⺻�� ����
            }
        });
    }
}
