using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretNoteClick : MonoBehaviour
{
    public GameObject[] gamePlayeObject;
    public GameObject ending;
    public Dialog dialog;

    public void NoteClick()
    {
        UIManager.Instance.EndingUI();
        // ���̾�α� ���� �� Debug.Log ȣ���� ���� �ݹ� ���
        DialogManager.Instance.OnDialogEnd += OnDialogEnd;
       
        DialogManager.Instance.PlayerMessageDialog(dialog);

    }

    private void OnDialogEnd()
    {
        DialogManager.Instance.OnDialogEnd -= OnDialogEnd;
        DisplayDay5ending();
    }
    public void DisplayDay5ending()
    {
        foreach (GameObject canvas in gamePlayeObject)
        {
            canvas.SetActive(false);
        }
        ending.SetActive(true);

    }
}
