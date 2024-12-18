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

    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text messageText;
    public TMP_Text UserUidText;

    private FirestoreController firestoreController;
    public GameManager gameManager;

    private FirebaseAuth auth;
    public FirebaseUser User { get; set; }

    private string message = "";
    public string uid = "";
    private bool isMessageUpdated = false;

    // ���� �޽��� ���� ��ųʸ�
    private Dictionary<string, string> errorMessages = new Dictionary<string, string>
    {
        { "8", "�� �̸����� �̹� ��� ���Դϴ�." },
        { "INVALID_EMAIL", "��ȿ���� ���� �̸��� �ּ��Դϴ�." },
        { "23", "��й�ȣ�� �ּ� 6�� �̻��̾�� �մϴ�." },
        { "11", "�̸��� �Ǵ� ��й�ȣ�� �߸��Ǿ����ϴ�." },
        { "1", "�̸��� �Ǵ� ��й�ȣ�� �߸��Ǿ����ϴ�." },
        { "??", "��й�ȣ�� �߸��Ǿ����ϴ�." },
        { "TOO_MANY_ATTEMPTS_TRY_LATER", "��� �� �ٽ� �õ����ּ���." }
    };

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
        messageText.text = string.Empty;

        InitializeFirebase();
    }


    void Update()
    {
        if (isMessageUpdated)
        {
            UpdateUI();
            isMessageUpdated = false;
        }
    }

    void OnDestroy()
    {
        if (auth != null)
        {
            auth.StateChanged -= AuthStateChanged;
            auth = null;
        }
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
    void CheckAndLogoutIfLoggedIn()
    {
        if (auth != null && auth.CurrentUser != null)
        {
            Debug.Log($"�̹� �α��ε� �����: {auth.CurrentUser.Email}");
            messageText.text = string.Empty;
            Logout();
        }
        else
        {
            Debug.Log("�α��� ���°� �ƴ�");
            messageText.text = string.Empty;
        }
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != User)
        {
            bool signedIn = User != auth.CurrentUser && auth.CurrentUser != null && auth.CurrentUser.IsValid();
            if (!signedIn && User != null)
            {
                Debug.Log("Signed out " + User.UserId);
                message = "Signed out: " + User.UserId;
                uid = "";
                isMessageUpdated = true;
            }
            User = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + User.UserId);
                message = "Signed in: " + User.Email;
                //UIManager.Instance.closeLoginUI();
                uid = User.UserId;
                isMessageUpdated = true;
                LoadGameState();
            }
        }
    }

    //OnClick �̺�Ʈ ����
    public void Register()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password)
            .ContinueWith(task => {
                if (task.IsCanceled || task.IsFaulted)
                {
                    string translatedMessage = GetTranslatedMessage(task.Exception);
                    message = "ȸ������ ����: " + translatedMessage;
                    isMessageUpdated = true;
                    return;
                }

                AuthResult authResult = task.Result;
                FirebaseUser newUser = authResult.User;
                message = "ȸ������ ����: " + newUser.Email;
                uid = newUser.UserId;
                isMessageUpdated = true;
            });
    }

    //OnClick �̺�Ʈ ����
    public void Login()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        auth.SignInWithEmailAndPasswordAsync(email, password)
            .ContinueWith(task => {
                if (task.IsCanceled || task.IsFaulted)
                {
                    string translatedMessage = GetTranslatedMessage(task.Exception);
                    message = "�α��� ����: " + translatedMessage;
                    isMessageUpdated = true;
                    return;
                }

                AuthResult authResult = task.Result;
                FirebaseUser newUser = authResult.User;
                message = "�α��� ����: " + newUser.Email;
                //UIManager.Instance.closeLoginUI();
                uid = newUser.UserId;
                isMessageUpdated = true;

                //�α��� �� �ҷ�����
                LoadGameState();
            });
    }

    public void Logout()
    {
        auth.SignOut();
        User = null;
        Debug.Log("�α׾ƿ���");
    }

    void OnApplicationQuit()
    {
        Logout(); // �� ���� �� �α׾ƿ� ó��

    }

    public string GetReturnUid()
    {
        return uid;
    }

    private void UpdateUI()
    {
        messageText.text = message;
        UserUidText.text = uid;
        emailInput.text = "";
        passwordInput.text = "";
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

    public bool IsLoggedIn()
    {
        return User != null; // User ������Ƽ�� Ȯ��
    }

    private string GetTranslatedMessage(AggregateException exception)
    {
        foreach (var innerException in exception.InnerExceptions)
        {
            if (innerException is FirebaseException firebaseException)
            {
                // ErrorCode �Ӽ��� ���� ���
                if (errorMessages.TryGetValue(firebaseException.ErrorCode.ToString(), out string translatedMessage))
                {
                    return translatedMessage;
                }
                else
                {
                    Debug.LogError($"Unmapped error code: {firebaseException.ErrorCode}");
                }
            }
        }
        return "�� �� ���� ������ �߻��߽��ϴ�.";
    }
}