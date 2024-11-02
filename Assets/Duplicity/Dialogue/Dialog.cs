using UnityEngine;

[CreateAssetMenu(fileName = "NewDialog", menuName = "Dialog")]
public class Dialog : ScriptableObject // ScriptableObject로 변경
{
    public string[] sentences; // 다이얼로그 문장 배열
}
