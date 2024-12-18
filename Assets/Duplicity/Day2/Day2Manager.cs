using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day2Manager : MonoBehaviour
{
    public Dialog[] dialog;
    public MissionTimer missionTimer;

    private int dialogIndex = 0;

    private InteractionManager interactionManager;
    private DialogManager dialogManager;

    void Start()
    {
        interactionManager = FindObjectOfType<InteractionManager>();
        if(DialogManager.Instance != null )
        {
            dialogManager = DialogManager.Instance;
            //���̾�α� 0
            dialogManager.PlayerMessageDialog(dialog[dialogIndex]);
            // ���̾�α� ���� �̺�Ʈ ����
            dialogManager.OnDialogEnd += HandleDialogEnd;
        }
        
    }
    private void HandleDialogEnd()
    {

        Debug.Log("���̾�αװ� ����Ǿ����ϴ�.");

        //���̾�α� ���� �� ������ ���� �̼� Ȱ��ȭ
        if(dialogIndex == 0)
        {
            interactionManager.isInteraction = true;
            missionTimer.isMissionActive = true;
            missionTimer.gameObject.SetActive(true);
        }
        //�̼� ���� ���� �� Ȱ��ȭ
        if (dialogIndex == 1)
        {
            //���� Ȱ��ȭ
            Debug.Log("���� Ȱ��ȭ");
            UIManager.Instance.ActiveMapIcon();
            MapManager.Instance.UnlockRegion("ShelterScene");
        }

    }
    public void CompleteItemCollected()
    {
        dialogIndex++;
        dialogManager.PlayerMessageDialog(dialog[dialogIndex]);
    }
    private void OnDestroy()
    {
        // �̺�Ʈ ���� ����
        if (dialogManager != null)
        {
            dialogManager.OnDialogEnd -= HandleDialogEnd;
        }
    }
}
