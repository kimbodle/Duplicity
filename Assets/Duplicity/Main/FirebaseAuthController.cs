using Firebase.Auth;
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
        messageText.text = string.Empty;

        /*// 로그인 상태 확인
        if (auth.CurrentUser != null)
        {
            // 사용자가 이미 로그인 되어 있으면 해당 상태를 유지하도록 하거나 로그아웃
            Debug.Log("User is already logged in: " + auth.CurrentUser.Email);
        }
        else
        {
            // 사용자가 로그인하지 않은 상태라면 초기 상태로 설정
            Debug.Log("No user is logged in.");
        }*/
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

    void InitializeFirebase()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Firebase가 사용 가능할 때 FirebaseAuth를 초기화합니다.
                auth = FirebaseAuth.DefaultInstance;
                firestoreController = FirestoreController.Instance;
                auth.StateChanged += AuthStateChanged;
                AuthStateChanged(this, null);
            }
            else
            {
                Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
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

    public void Register()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password)
            .ContinueWith(task => {
                if (task.IsCanceled || task.IsFaulted)
                {
                    message = "Sign up failed: " + task.Exception?.Message;
                    isMessageUpdated = true;
                    return;
                }

                AuthResult authResult = task.Result;
                FirebaseUser newUser = authResult.User;
                message = "Sign up successful: " + newUser.Email;
                uid = newUser.UserId;
                isMessageUpdated = true;
            });
    }

    public void Login()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        auth.SignInWithEmailAndPasswordAsync(email, password)
            .ContinueWith(task => {
                if (task.IsCanceled || task.IsFaulted)
                {
                    message = "Login failed: " + task.Exception?.Message;
                    isMessageUpdated = true;
                    return;
                }

                AuthResult authResult = task.Result;
                FirebaseUser newUser = authResult.User;
                message = "Login successful: " + newUser.Email;
                //Map 하기 위해 UIManager 코드 추가 해보면 알게 된 사실인데 이 Login()함수 코드가 로그인 할때 안쓰임....(?)
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

    private void OnGameStateLoaded(int currentDay, string currentSceneName, string currentTask, Dictionary<string, bool> gameState) //게임매니저의 게임상태 업데이트 함수호출
    {
        if (gameManager != null)
        {
            gameManager.InitializeGameState(currentDay, currentSceneName, currentTask, gameState);
        }
    }

    public bool IsLoggedIn()
    {
        return User != null; // User 프로퍼티로 확인
    }
}
