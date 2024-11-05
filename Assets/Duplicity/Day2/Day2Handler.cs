using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day2Handler : MonoBehaviour
{
    public Dialog[] dialog;
    public Sprite characterSprite;
    public GameObject missionTimer;
    private int dialogIndex = 0;

    InteractionManager interactionManager;

    private DialogManager dialogManager;

    void Start()
    {
        dialogManager = DialogManager.Instance;
        interactionManager= FindObjectOfType<InteractionManager>();

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
            missionTimer.GetComponent<MissionTimer>().isMissionActive = true;
            missionTimer.SetActive(true);
        }

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
