using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day5Manager : MonoBehaviour
{
    public Dialog Intro;
    public Image BlackImage;
    public PhotoMissionTimer timer;
    void Start()
    {
        BlackImage.gameObject.SetActive(true);
        timer.gameObject.SetActive(true);
        
        if(DialogManager.Instance != null)
        {
            DialogManager.Instance.OnDialogEnd += HandleDialogEnd;
            DialogManager.Instance.PlayerMessageDialog(Intro);
        }   
    }

    private void HandleDialogEnd()
    {
        //페이드 매니저 기능 추가
        BlackImage.gameObject.SetActive(false);
        timer.isMissionActive = true;
        if (DialogManager.Instance != null)
        {
            // 이벤트 구독 해제
            DialogManager.Instance.OnDialogEnd -= HandleDialogEnd;
        }
    }
}
