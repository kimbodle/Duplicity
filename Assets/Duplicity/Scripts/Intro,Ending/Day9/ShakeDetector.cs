using UnityEngine;
using System;

public class ShakeDetector : MonoBehaviour
{
    public float shakeThreshold = 2.0f; // 흔들기 감지 민감도
    public float shakeCooldown = 0.5f; // 두 번의 흔들기 사이 최소 간격
    private float lastShakeTime = 0.0f; // 마지막 흔들린 시간

    public event Action OnShake; // 흔들기 이벤트

    private void Update()
    {
        DetectShake();
    }

    private void DetectShake()
    {
        // 현재 가속도 값
        Vector3 acceleration = Input.acceleration;

        // 가속도 크기가 임계값을 초과하면 흔들기로 간주
        if (acceleration.sqrMagnitude > shakeThreshold * shakeThreshold)
        {
            float currentTime = Time.time;
            if (currentTime - lastShakeTime > shakeCooldown) // 최소 간격 체크
            {
                lastShakeTime = currentTime;
                OnShake?.Invoke(); // 흔들기 이벤트 호출
            }
        }
    }
}
