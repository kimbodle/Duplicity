using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionTimer : MonoBehaviour
{
    public float timeLimit = 120f; // 제한 시간 (초)
    public TMP_Text timerText; // 타이머 UI
    public bool isMissionActive = false; // 미션 활성화 상태

    private TMP_Text textMeshPro;
    private void Start()
    {
        textMeshPro = GetComponentInChildren<TMP_Text>();
        textMeshPro.gameObject.SetActive(false);
    }
    void Update()
    {
        if (isMissionActive)
        {
            textMeshPro.gameObject.SetActive(true);
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
        Debug.Log("Mission Failed");
        // 여기서 미션 실패 처리 (게임 오버 화면 또는 재시도 버튼 등)
    }

    public void CompleteMission()
    {
        isMissionActive = false;
        Debug.Log("Mission Complete");
        // 미션 완료 처리
        FindObjectOfType<Day2Controller>().CompleteTask("ItemCollected");
        FindObjectOfType<Day2Manager>().CompleteItemCollected();
    }
}
