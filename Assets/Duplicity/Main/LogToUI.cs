using UnityEngine;
using TMPro;
using System.IO;

public class LogToUI : MonoBehaviour
{
    public TMP_Text logText; // UI Text 컴포넌트
    private string logFilePath;

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void Start()
    {
        // 로그 파일 경로 설정
        logFilePath = Path.Combine(Application.persistentDataPath, "log.txt");

        // 초기 로그 메시지
        Debug.Log("이것은 로그 메시지입니다."); // 일반 로그
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // UI에 로그 추가
        logText.text += logString + "\n";

        // 로그를 파일에 추가
        File.AppendAllText(logFilePath, logString + "\n");
    }
}
