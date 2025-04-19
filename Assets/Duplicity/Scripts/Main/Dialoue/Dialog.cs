using UnityEngine;

[CreateAssetMenu(fileName = "NewDialog", menuName = "Dialog")]
public class Dialog : ScriptableObject // ScriptableObject�� ����
{
    [TextArea(1, 2)]
    public string[] sentences; // ���̾�α� ���� �迭
    public bool[] isPlayerSpeaking; // �� ������ ȭ�ڰ� �÷��̾����� ����
    public Sprite characterSprite;
}
