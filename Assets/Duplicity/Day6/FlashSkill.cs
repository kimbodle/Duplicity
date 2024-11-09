using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSkill : MonoBehaviour
{
    public GameObject flashEffect; // �÷��� ��ų ����Ʈ (��� ������ �̹���)
    public float flashRange = 2f; // �÷��� ��ų ����
    public LayerMask guardLayer; // ��� ���̾�

    //OnClick �̺�Ʈ ����
    private void UseFlashSkill()
    {
        flashEffect.SetActive(true); // �÷��� ����Ʈ Ȱ��ȭ
        Collider2D[] guardsInRange = Physics2D.OverlapCircleAll(transform.position, flashRange, guardLayer);

        foreach (Collider2D guard in guardsInRange)
        {
            Debug.Log("Guard hit by flash: " + guard.name); // ������ ����� �̸� ���
            guard.GetComponent<PatrolGuard>().StunGuard(); // ���� �� ��� ����ȭ
        }

        Invoke("EndFlash", 0.5f); // 0.5�� �� �÷��� ����
    }


    private void EndFlash()
    {
        flashEffect.SetActive(false); // �÷��� ����Ʈ ��Ȱ��ȭ
    }
}
