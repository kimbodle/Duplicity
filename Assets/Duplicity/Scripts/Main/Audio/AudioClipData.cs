using UnityEngine;

[CreateAssetMenu(fileName = "NewAudioClip", menuName = "Audio/Effect")]
public class AudioClipData : ScriptableObject
{
    public string clipName; // 효과음 이름
    public AudioClip clip;  // 효과음 파일
}

