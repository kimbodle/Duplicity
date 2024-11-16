using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionTimer : MonoBehaviour
{
    public float timeLimit = 120f; // ���� �ð� (��)
    public TMP_Text timerText; // Ÿ�̸� UI
    public bool isMissionActive = false; // �̼� Ȱ��ȭ ����
    public Dialog dialog;

    private int currentDay = 0;

    private void Start()
    {
        currentDay = GameManager.Instance.GetCurrentDay();
    }
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
        if(currentDay == 2)
        {
            if(EndingManager.Instance != null)
            {
                EndingManager.Instance.LoadEnding("GameOver", "Ÿ�� ����", 2);
            }
        }
        if(currentDay == 8)
        {
            if(EndingManager.Instance != null)
            {
                EndingManager.Instance.LoadEnding("GameOver", "Ÿ�� ����", 8);
            }
        }
    }

    public void CompleteMission()
    {
        isMissionActive = false;
        Debug.Log("�̼� ����");
        if (currentDay == 2)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GetCurrentDayController().CompleteTask("ItemCollected");
                FindObjectOfType<Day2Manager>().CompleteItemCollected();
            }
        }
        if (currentDay == 8)
        {
            if (GameManager.Instance != null)
            {
                
                GameManager.Instance.GetCurrentDayController().CompleteTask("ItemCollected");

                DialogManager.Instance.OnDialogEnd += HandleDialogEnd;

                DialogManager.Instance.PlayerMessageDialog(dialog);
                
            }
        }
    }
    private void HandleDialogEnd()
    {
        //���̵� �� �ƿ� �߰�
        GameManager.Instance.CompleteTask("Day9Scene");
    }
}
