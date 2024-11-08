using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day5Ending : MonoBehaviour
{
    public Sprite wakeupImage;
    public Dialog dialog;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDay5EndingDialog()
    {
        DialogManager.Instance.StartDialog(dialog, wakeupImage);

        // ���̾�α� ���� �� Debug.Log ȣ���� ���� �ݹ� ���
        DialogManager.Instance.OnDialogEnd += OnDialogEnd;

        GameManager.Instance.GetCurrentDayController().CompleteTask("WakeUp");
    }

    private void OnDialogEnd()
    {
        // �ݹ��� �� ���� �����ϵ��� ��� ����
        DialogManager.Instance.OnDialogEnd -= OnDialogEnd;

        GameManager.Instance.CompleteTask("ShelterScene");
    }
}
