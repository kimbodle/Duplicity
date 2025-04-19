using UnityEngine;
using System;

public class ShakeDetector : MonoBehaviour
{
    public float shakeThreshold = 2.0f; // ���� ���� �ΰ���
    public float shakeCooldown = 0.5f; // �� ���� ���� ���� �ּ� ����
    private float lastShakeTime = 0.0f; // ������ ��鸰 �ð�

    public event Action OnShake; // ���� �̺�Ʈ

    private void Update()
    {
        DetectShake();
    }

    private void DetectShake()
    {
        // ���� ���ӵ� ��
        Vector3 acceleration = Input.acceleration;

        // ���ӵ� ũ�Ⱑ �Ӱ谪�� �ʰ��ϸ� ����� ����
        if (acceleration.sqrMagnitude > shakeThreshold * shakeThreshold)
        {
            float currentTime = Time.time;
            if (currentTime - lastShakeTime > shakeCooldown) // �ּ� ���� üũ
            {
                lastShakeTime = currentTime;
                OnShake?.Invoke(); // ���� �̺�Ʈ ȣ��
            }
        }
    }
}
