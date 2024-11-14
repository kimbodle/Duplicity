using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day8Manager : MonoBehaviour
{
    public Dialog dialog;
    public MissionTimer missionTimer;

    // Start is called before the first frame update
    void Start()
    {
        if (DialogManager.Instance != null)
        {
            DialogManager.Instance.OnDialogEnd += HandleDialogEnd;
            DialogManager.Instance.PlayerMessageDialog(dialog);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void HandleDialogEnd()
    {
        Debug.Log("���̾�αװ� ����Ǿ����ϴ�.");
        missionTimer.gameObject.SetActive(true);
        missionTimer.isMissionActive = true;
    }
}
