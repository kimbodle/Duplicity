using TMPro;
using UnityEngine;

public class FirebaseUI : MonoBehaviour
{
    public GameObject LoginUI;

    [Header("Interactive")]
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text messageText;
    public TMP_Text UserUidText;

    private void Start()
    {
        LoginUI.SetActive(false);
    }

    private void UpdateUI(string message, string uid)
    {
        messageText.text = message;
        UserUidText.text = uid;
    }

    public void OnRegister()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        FirebaseAuthController.Instance.Register(email, password, result =>
        {
            UpdateUI(result, FirebaseAuthController.Instance.Uid ?? string.Empty); // UID�� ������ �� ���ڿ�
        });
    }

    public void OnLogin()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        FirebaseAuthController.Instance.Login(email, password, result =>
        {
            UpdateUI(result, FirebaseAuthController.Instance.Uid ?? string.Empty); // UID�� ������ �� ���ڿ�
        });
    }

    public void OnLogout()
    {
        FirebaseAuthController.Instance.Logout();
        UpdateUI("�α׾ƿ���", string.Empty);
    }

    //Login
    public void OpenLoginUI()
    {
        LoginUI.SetActive(true);
    }

    public void CloseLoginUI()
    {
        LoginUI.SetActive(false);
    }
}
