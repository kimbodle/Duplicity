using TMPro;
using UnityEngine;

public class FirebaseUIManager : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text messageText;
    public TMP_Text UserUidText;

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
            UpdateUI(result, FirebaseAuthController.Instance.Uid ?? string.Empty); // UID가 없으면 빈 문자열
        });
    }

    public void OnLogin()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        FirebaseAuthController.Instance.Login(email, password, result =>
        {
            UpdateUI(result, FirebaseAuthController.Instance.Uid ?? string.Empty); // UID가 없으면 빈 문자열
        });
    }

    public void OnLogout()
    {
        FirebaseAuthController.Instance.Logout();
        UpdateUI("로그아웃됨", string.Empty);
    }
}
