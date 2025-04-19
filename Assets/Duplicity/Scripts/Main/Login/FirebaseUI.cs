using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseUI : MonoBehaviour
{
    public GameObject LoginUI;

    [Header("Interactive")]
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text messageText;
    public TMP_Text UserUidText;

    public Toggle rememberMeToggle;

    private void Start()
    {
        LoginUI.SetActive(false);

        // ���� ���� �� ���� �α��� ���� �ҷ�����
        if (PlayerPrefs.GetInt("RememberMe", 0) == 1)  // RememberMe�� 1�� ���
        {
            string email = PlayerPrefs.GetString("Email", "");
            string password = PlayerPrefs.GetString("Password", "");
            emailInput.text = email;
            passwordInput.text = password;
        }
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
            RememberInfo(email, password);

            UpdateUI(result, FirebaseAuthController.Instance.Uid ?? string.Empty); // UID�� ������ �� ���ڿ�
        });
    }

    public void OnLogin()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        FirebaseAuthController.Instance.Login(email, password, result =>
        {
            RememberInfo(email, password);

            UpdateUI(result, FirebaseAuthController.Instance.Uid ?? string.Empty); // UID�� ������ �� ���ڿ�
        });
    }

    private void RememberInfo(string email, string password)
    {
        // �α��� ���� �� ��� ���ο� ���� PlayerPrefs�� ����
        if (rememberMeToggle.isOn)
        {
            PlayerPrefs.SetInt("RememberMe", 1);
            PlayerPrefs.SetString("Email", email);
            PlayerPrefs.SetString("Password", password);
        }
        else
        {
            PlayerPrefs.SetInt("RememberMe", 0);
            PlayerPrefs.DeleteKey("Email");
            PlayerPrefs.DeleteKey("Password");
        }
    }

    public void OnLogout()
    {
        FirebaseAuthController.Instance.Logout();
        UpdateUI("�α׾ƿ���", string.Empty);

        // �α׾ƿ� �� PlayerPrefs �ʱ�ȭ
        PlayerPrefs.SetInt("RememberMe", 0);
        PlayerPrefs.DeleteKey("Email");
        PlayerPrefs.DeleteKey("Password");
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
