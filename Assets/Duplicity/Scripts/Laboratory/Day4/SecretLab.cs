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

        // 다이얼로그 종료 시 패널 비활성화를 위해 이벤트 등록
        DialogManager.Instance.OnDialogEnd += DisableSecretLabPanel;

        // 다이얼로그 시작
        DialogManager.Instance.PlayerMessageDialog(documentAfterDialog);
    }

    private void DisableSecretLabPanel()
    {
        // 다이얼로그 종료 시 패널 비활성화
        secretLab.SetActive(false);

        // 이벤트 해제하여 중복 호출 방지
        DialogManager.Instance.OnDialogEnd -= DisableSecretLabPanel;
    }
}
