using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    public Slider bgmSlider; // ������� ���� �����̴�
    public Slider sfxSlider; // ȿ���� ���� �����̴�

    private void Start()
    {
        // �����̴� �ʱⰪ ����
        bgmSlider.value = AudioManager.Instance.bgmVolume;
        sfxSlider.value = AudioManager.Instance.sfxVolume;

        // �����̴� �� ���� �� AudioManager�� ���� ���� �޼��� ȣ��
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void SetBGMVolume(float volume)
    {
        AudioManager.Instance.SetBGMVolume(volume);
    }

    private void SetSFXVolume(float volume)
    {
        AudioManager.Instance.SetSFXVolume(volume);
    }
}
