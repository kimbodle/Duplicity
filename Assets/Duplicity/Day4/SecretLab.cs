using UnityEngine;

public class SecretLab : MonoBehaviour
{
    public GameObject secretLab;
    public Dialog enterSecretDialog;
    public Dialog documentAfterDialog;
    //private bool isFirst;

    void Start()
    {
        secretLab.SetActive(false);
    }

    public void SecretLabDisplay()
    {
        secretLab.SetActive(true);
        GameManager.Instance.GetCurrentDayController().CompleteTask("SecretLabOpen");
        DialogManager.Instance.PlayerMessageDialog(enterSecretDialog);
    }

    public void OnClickCloseButton()
    {
        secretLab.SetActive(false);
        DialogManager.Instance.PlayerMessageDialog(documentAfterDialog);
        
    }
}
