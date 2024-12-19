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
            UpdateUI(result, FirebaseAuthController.Instance.Uid);
        });
    }

    public void OnLogin()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        FirebaseAuthController.Instance.Login(email, password, result =>
        {
            UpdateUI(result, FirebaseAuthController.Instance.Uid);
        });
    }

    public void OnLogout()
    {
        FirebaseAuthController.Instance.Logout();
        UpdateUI("·Î±×¾Æ¿ôµÊ", string.Empty);
    }
}
