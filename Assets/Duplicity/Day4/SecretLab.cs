using UnityEngine;

public class SecretLab : MonoBehaviour
{
    public GameObject secretLab;
    public Dialog dialog;

    void Start()
    {
        secretLab.SetActive(false);
    }

    public void SecretLabDisplay()
    {
        secretLab.SetActive(true);
        GameManager.Instance.GetCurrentDayController().CompleteTask("SecretLabOpen");
        DialogManager.Instance.PlayerMessageDialog(dialog);
    }

    public void OnClickCloseButton()
    {
        secretLab.SetActive(false);
    }
}
