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
        DialogManager.Instance.PlayerMessageDialog(dialog);
    }
}
