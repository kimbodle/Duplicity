using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day0Test : MonoBehaviour
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

        dialogManager = FindObjectOfType<DialogManager>();
        dialogManager.characterImage.sprite = mainCharacterSprite;

        // ���̾�α� ���� �̺�Ʈ ����
        dialogManager.OnDialogEnd += HandleDialogEnd;
    }

    public void OnClickIntroTestButton()
    {
        DialogManager.Instance.StartDialog(dialogIntro, mainCharacterSprite);
    }

    private void HandleDialogEnd()
    {
        // ���̾�αװ� ������ �� ������ �ڵ�
        Debug.Log("���̾�αװ� ����Ǿ����ϴ�.");
        // �߰����� ó���� ���⿡ �ۼ�
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
