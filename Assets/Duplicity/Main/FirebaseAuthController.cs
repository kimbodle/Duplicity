using Firebase;
using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirebaseAuthController : MonoBehaviour
{
    public static FirebaseAuthController Instance { get; private set; } // 싱글톤 인스턴스

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

    // 오류 메시지 번역 딕셔너리
    private Dictionary<string, string> errorMessages = new Dictionary<string, string>
    {
        { "8", "이 이메일은 이미 사용 중입니다." },
        { "INVALID_EMAIL", "유효하지 않은 이메일 주소입니다." },
        { "23", "비밀번호는 최소 6자 이상이어야 합니다." },
        { "11", "이메일 또는 비밀번호가 잘못되었습니다." },
        { "1", "이메일 또는 비밀번호가 잘못되었습니다." },
        { "??", "비밀번호가 잘못되었습니다." },
        { "TOO_MANY_ATTEMPTS_TRY_LATER", "잠시 후 다시 시도해주세요." }
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

    // Firebase 초기화 메서드
    void InitializeFirebase()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Firebase가 사용 가능할 때 FirebaseAuth 초기화
                auth = FirebaseAuth.DefaultInstance;
                firestoreController = FirestoreController.Instance;
                auth.StateChanged += AuthStateChanged;
                AuthStateChanged(this, null);

                // FirebaseAuth가 초기화된 후 로그인 상태 확인
                CheckAndLogoutIfLoggedIn();
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
        });
    }
    // 로그인 상태 확인 후 로그아웃
    void CheckAndLogoutIfLoggedIn()
    {
        if (auth != null && auth.CurrentUser != null)
        {
            Debug.Log($"이미 로그인된 사용자: {auth.CurrentUser.Email}");
            messageText.text = string.Empty;
            Logout();
        }
        else
        {
            Debug.Log("로그인 상태가 아님");
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

    //OnClick 이벤트 연결
    public void Register()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password)
            .ContinueWith(task => {
                if (task.IsCanceled || task.IsFaulted)
                {
                    string translatedMessage = GetTranslatedMessage(task.Exception);
                    message = "회원가입 실패: " + translatedMessage;
                    isMessageUpdated = true;
                    return;
                }

                AuthResult authResult = task.Result;
                FirebaseUser newUser = authResult.User;
                message = "회원가입 성공: " + newUser.Email;
                uid = newUser.UserId;
                isMessageUpdated = true;
            });
    }

    //OnClick 이벤트 연결
    public void Login()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        auth.SignInWithEmailAndPasswordAsync(email, password)
            .ContinueWith(task => {
                if (task.IsCanceled || task.IsFaulted)
                {
                    string translatedMessage = GetTranslatedMessage(task.Exception);
                    message = "로그인 실패: " + translatedMessage;
                    isMessageUpdated = true;
                    return;
                }

                AuthResult authResult = task.Result;
                FirebaseUser newUser = authResult.User;
                message = "로그인 성공: " + newUser.Email;
                //UIManager.Instance.closeLoginUI();
                uid = newUser.UserId;
                isMessageUpdated = true;

                //로그인 후 불러오기
                LoadGameState();
            });
    }

    public void Logout()
    {
        auth.SignOut();
        User = null;
        Debug.Log("로그아웃됨");
    }

    void OnApplicationQuit()
    {
        Logout(); // 앱 종료 시 로그아웃 처리

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
        firestoreController.LoadGameState(OnGameStateLoaded); //아래 함수를 파라미터로 같이 전달
    }

    private void OnGameStateLoaded(int currentDay, string currentSceneName, string currentTask,
        Dictionary<string, bool> gameState, Dictionary<string, bool> endingAlbum) //게임매니저의 게임상태 업데이트 함수호출
    {
        if (gameManager != null)
        {
            gameManager.InitializeGameState(currentDay, currentSceneName, currentTask, gameState, endingAlbum);
        }
    }

    public bool IsLoggedIn()
    {
        return User != null; // User 프로퍼티로 확인
    }

    private string GetTranslatedMessage(AggregateException exception)
    {
        foreach (var innerException in exception.InnerExceptions)
        {
            if (innerException is FirebaseException firebaseException)
            {
                // ErrorCode 속성을 직접 사용
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
        return "알 수 없는 오류가 발생했습니다.";
    }
}