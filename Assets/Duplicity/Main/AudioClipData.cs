using UnityEngine;

[CreateAssetMenu(fileName = "NewAudioClip", menuName = "Audio/Effect")]
public class AudioClipData : ScriptableObject
{
    public string clipName; // ȿ���� �̸�
    public AudioClip clip;  // ȿ���� ����
}

