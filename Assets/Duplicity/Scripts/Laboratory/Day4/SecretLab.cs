using UnityEngine;

public class SecretLab : MonoBehaviour
{
    public GameObject secretLab;
    public Dialog enterSecretDialog;
    public Dialog documentAfterDialog;

    void Start()
    {
        secretLab.SetActive(false);
    }

    public void SecretLabDisplay()
    {
        secretLab.SetActive(true);
        DialogManager.Instance.PlayerMessageDialog(enterSecretDialog);
    }

    public void OnClickCloseButton()
    {
        GetComponentInChildren<SecretDocument>().secretDocumentImage.SetActive(false);

        // ���̾�α� ���� �� �г� ��Ȱ��ȭ�� ���� �̺�Ʈ ���
        DialogManager.Instance.OnDialogEnd += DisableSecretLabPanel;

        // ���̾�α� ����
        DialogManager.Instance.PlayerMessageDialog(documentAfterDialog);
    }

    private void DisableSecretLabPanel()
    {
        // ���̾�α� ���� �� �г� ��Ȱ��ȭ
        secretLab.SetActive(false);

        // �̺�Ʈ �����Ͽ� �ߺ� ȣ�� ����
        DialogManager.Instance.OnDialogEnd -= DisableSecretLabPanel;
    }
}
