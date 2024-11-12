using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RegenStorageMissionTimer : MonoBehaviour
{
    public float timeLimit = 120f; // ���� �ð� (��)
    public TMP_Text timerText; // Ÿ�̸� UI
    public bool isMissionActive = false; // �̼� Ȱ��ȭ ����

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
        Debug.Log("�̼� ����");
        EndingManager.Instance.LoadEnding("GameOver", "Ÿ�� ����", 6);
    }

    public void CompleteMission()
    {
        isMissionActive = false;
        Debug.Log("�̼� ����");
    }
}
