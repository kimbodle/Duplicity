using UnityEngine;
using TMPro;
using System.IO;

public class LogToUI : MonoBehaviour
{
    public TMP_Text logText; // UI Text ������Ʈ
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
        // �α� ���� ��� ����
        logFilePath = Path.Combine(Application.persistentDataPath, "log.txt");

        // �ʱ� �α� �޽���
        Debug.Log("�̰��� �α� �޽����Դϴ�."); // �Ϲ� �α�
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // UI�� �α� �߰�
        logText.text += logString + "\n";

        // �α׸� ���Ͽ� �߰�
        File.AppendAllText(logFilePath, logString + "\n");
    }
}
