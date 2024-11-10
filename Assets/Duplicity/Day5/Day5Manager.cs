using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day5Manager : MonoBehaviour
{
    public Dialog Intro;
    public Image BlackImage;
    public PhotoMissionTimer timer;
    // Start is called before the first frame update
    void Start()
    {
        BlackImage.gameObject.SetActive(true);
        timer.gameObject.SetActive(true );
        // 다이얼로그 종료 이벤트 구독
        DialogManager.Instance.OnDialogEnd += HandleDialogEnd;

        DialogManager.Instance.PlayerMessageDialog(Intro);
    }

    private void HandleDialogEnd()
    {
        //페이드 매니저 기능 추가
        BlackImage.gameObject.SetActive(false);
        timer.isMissionActive = true;

        // 이벤트 구독 해제
        DialogManager.Instance.OnDialogEnd -= HandleDialogEnd;
    }
}
