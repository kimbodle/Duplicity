using Firebase;
using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirebaseAuthController : MonoBehaviour
{
    public static FirebaseAuthController Instance { get; private set; } // �̱��� �ν��Ͻ�

    private FirebaseAuth auth;
    public FirebaseUser User { get; set; }

    private FirestoreController firestoreController;
    public string Uid => User?.UserId;

    public GameManager gameManager;
    
    void Awake()
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
        InitializeFirebase();
    }

    // Firebase �ʱ�ȭ �޼���
    void InitializeFirebase()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Firebase�� ��� ������ �� FirebaseAuth �ʱ�ȭ
                auth = FirebaseAuth.DefaultInstance;
                firestoreController = FirestoreController.Instance;
                auth.StateChanged += AuthStateChanged;
                AuthStateChanged(this, null);

                // FirebaseAuth�� �ʱ�ȭ�� �� �α��� ���� Ȯ��
                CheckAndLogoutIfLoggedIn();
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
        });
    }
    // �α��� ���� Ȯ�� �� �α׾ƿ�
    private void CheckAndLogoutIfLoggedIn()
    {
        if (auth != null && auth.CurrentUser != null)
        {
            Debug.Log($"�̹� �α��ε� �����: {auth.CurrentUser.Email}");
            Logout();
        }
        else
        {
            Debug.Log("�α��� ���°� �ƴ�");
        }
    }

    private void AuthStateChanged(object sender, EventArgs eventArgs)
    {
        if (auth.CurrentUser != User)
        {
            bool signedIn = User != auth.CurrentUser && auth.CurrentUser != null && auth.CurrentUser.IsValid();
            if (!signedIn && User != null)
            {
                User = null; // ���� ����� ���� �ʱ�ȭ
            }

            User = auth.CurrentUser;

            if (signedIn)
            {
                LoadGameState(); // �α��� �� ���� ���� �ε�
            }
        }
    }

    //OnClick �̺�Ʈ ����
    public void Register(string email, string password, Action<string> callback)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                string errorMessage = GetTranslatedMessage(task.Exception);
                callback?.Invoke($"ȸ������ ����: {errorMessage}");
            }
            else
            {
                callback?.Invoke("ȸ������ ����");
            }
        });
    }

    //OnClick �̺�Ʈ ����
    public void Login(string email, string password, Action<string> callback)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                string errorMessage = GetTranslatedMessage(task.Exception);
                callback?.Invoke($"�α��� ����: {errorMessage}");
            }
            else
            {
                callback?.Invoke("�α��� ����");

                //�α��� �� �ҷ�����
                LoadGameState();
            }
        });
    }

    public void Logout()
    {
        auth.SignOut();
        User = null;
        Debug.Log("�α׾ƿ���");
    }

    private string GetTranslatedMessage(AggregateException exception)
    {
        Dictionary<string, string> errorMessages = new Dictionary<string, string>
        {
            { "8", "�� �̸����� �̹� ��� ���Դϴ�." },
            { "INVALID_EMAIL", "��ȿ���� ���� �̸��� �ּ��Դϴ�." },
            { "23", "��й�ȣ�� �ּ� 6�� �̻��̾�� �մϴ�." },
            { "11", "�̸��� �Ǵ� ��й�ȣ�� �߸��Ǿ����ϴ�." },
            { "1", "�̸��� �Ǵ� ��й�ȣ�� �߸��Ǿ����ϴ�." },
            { "??", "��й�ȣ�� �߸��Ǿ����ϴ�." },
            { "TOO_MANY_ATTEMPTS_TRY_LATER", "��� �� �ٽ� �õ����ּ���." }
        };

        foreach (var innerException in exception.InnerExceptions)
        {
            if (innerException is FirebaseException firebaseException)
            {
                if (errorMessages.TryGetValue(firebaseException.ErrorCode.ToString(), out string translatedMessage))
                {
                    return translatedMessage;
                }
                else
                {
                    Debug.LogError($"���ε��� ���� ���� �ڵ�: {firebaseException.ErrorCode}");
                }
            }
        }
        return "�� �� ���� ������ �߻��߽��ϴ�.";
    }


    private void LoadGameState()
    {
        firestoreController.LoadGameState(OnGameStateLoaded); //�Ʒ� �Լ��� �Ķ���ͷ� ���� ����
    }

    private void OnGameStateLoaded(int currentDay, string currentSceneName, string currentTask,
        Dictionary<string, bool> gameState, Dictionary<string, bool> endingAlbum) //���ӸŴ����� ���ӻ��� ������Ʈ �Լ�ȣ��
    {
        if (gameManager != null)
        {
            gameManager.InitializeGameState(currentDay, currentSceneName, currentTask, gameState, endingAlbum);
        }
    }

    void OnApplicationQuit()
    {
        Logout(); // �� ���� �� �α׾ƿ� ó��
    }

    public bool IsLoggedIn()
    {
        return User != null; // User ������Ƽ�� Ȯ��
    }
}