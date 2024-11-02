using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day2Handler : MonoBehaviour
{
    public Dialog[] dialog;
    public Sprite characterSprite;
    private int dialogIndex = 0;

    InteractionManager interactionManager;
    MissionTimer missionTimer;
    DialogManager dialogManager;

    void Start()
    {
        dialogManager = DialogManager.Instance;
        interactionManager= FindObjectOfType<InteractionManager>();
        missionTimer = FindObjectOfType<MissionTimer>();

        //���̾�α� 0
        dialogManager.StartDialog(dialog[dialogIndex], characterSprite);
        // ���̾�α� ���� �̺�Ʈ ����
        dialogManager.OnDialogEnd += HandleDialogEnd;
    }
    private void HandleDialogEnd()
    {

        Debug.Log("���̾�αװ� ����Ǿ����ϴ�.");
        //day2Controller ���� ������ ���� �̼� Ȱ��ȭ �ϱ�
        if(dialogIndex == 0)
        {
            interactionManager.isInteraction = true;
            missionTimer.isMissionActive = true;
        }

        if (dialogIndex == 1)
        {
            //���� Ȱ��ȭ
            Debug.Log("���� Ȱ��ȭ");
            UIManager.Instance.OpenMapIcon();
            MapManager.Instance.UnlockRegion("ShelterScene");
        }

    }
    public void CompleteItemCollected()
    {
        dialogIndex++;
        dialogManager.StartDialog(dialog[dialogIndex], characterSprite);
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
