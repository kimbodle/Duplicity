using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RegenStorageMissionTimer : MonoBehaviour
{
    public float timeLimit = 120f; // 제한 시간 (초)
    public TMP_Text timerText; // 타이머 UI
    public bool isMissionActive = false; // 미션 활성화 상태

    // Update is called once per frame
    void Update()
    {
        if (isMissionActive)
        {
            timeLimit -= Time.deltaTime;
            UpdateTimerUI();

            if (timeLimit <= 0)
            {
                isMissionActive = false;
                MissionFailed();
            }
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeLimit / 60f);
        int seconds = Mathf.FloorToInt(timeLimit % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void MissionFailed()
    {
        Debug.Log("미션 실패");
        EndingManager.Instance.LoadEnding("GameOver", "타임 오버", 6);
    }

    public void CompleteMission()
    {
        isMissionActive = false;
        Debug.Log("미션 성공");
    }
}
