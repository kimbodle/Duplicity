using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day0IntroPlay : MonoBehaviour
{
    public Sprite mainCharacterSprite; // ���ΰ� �̹���
    public Dialog dialogIntro; // �����Ϳ��� ������ Dialog ��ü
    //Dialog dialogIntro;

    Day0Controller day0controller;
    DialogManager dialogManager;

    private void Start()
    {
        //dialogIntro = GetComponent<Dialog>();
        day0controller = FindObjectOfType<Day0Controller>();

        dialogManager = DialogManager.Instance;
        dialogManager.characterImage.sprite = mainCharacterSprite;

        // ���̾�α� ���� �̺�Ʈ ����
        dialogManager.OnDialogEnd += HandleDialogEnd;

        DialogManager.Instance.StartDialog(dialogIntro, mainCharacterSprite);
    }

    public void OnClickIntroTestButton()
    {
        DialogManager.Instance.StartDialog(dialogIntro, mainCharacterSprite);
    }

    private void HandleDialogEnd()
    {
        // ���̾�αװ� ������ �� ������ �ڵ�
        Debug.Log("���̾�αװ� ����Ǿ����ϴ�.");

        day0controller.CompleteTask("Intro");
        GameManager.Instance.CompleteTask();
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
