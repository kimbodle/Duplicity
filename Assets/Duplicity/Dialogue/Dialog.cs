using UnityEngine;

[CreateAssetMenu(fileName = "NewDialog", menuName = "Dialog")]
public class Dialog : ScriptableObject // ScriptableObject로 변경
{
    [TextArea(1, 2)]
    public string[] sentences; // 다이얼로그 문장 배열
    public bool[] isPlayerSpeaking; // 각 문장의 화자가 플레이어인지 여부
    public Sprite characterSprite;
}
