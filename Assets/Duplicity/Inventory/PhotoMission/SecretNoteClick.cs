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
        // 다이얼로그 종료 시 Debug.Log 호출을 위한 콜백 등록
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
