using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSkill : MonoBehaviour
{
    public GameObject flashEffect; // 플래시 스킬 이펙트 (흰색 반투명 이미지)
    public float flashRange = 2f; // 플래시 스킬 범위
    public LayerMask guardLayer; // 경비병 레이어

    //OnClick 이벤트 연결
    private void UseFlashSkill()
    {
        flashEffect.SetActive(true); // 플래시 이펙트 활성화
        Collider2D[] guardsInRange = Physics2D.OverlapCircleAll(transform.position, flashRange, guardLayer);

        foreach (Collider2D guard in guardsInRange)
        {
            Debug.Log("Guard hit by flash: " + guard.name); // 감지된 경비병의 이름 출력
            guard.GetComponent<PatrolGuard>().StunGuard(); // 범위 내 경비병 무력화
        }

        Invoke("EndFlash", 0.5f); // 0.5초 후 플래시 종료
    }


    private void EndFlash()
    {
        flashEffect.SetActive(false); // 플래시 이펙트 비활성화
    }
}
