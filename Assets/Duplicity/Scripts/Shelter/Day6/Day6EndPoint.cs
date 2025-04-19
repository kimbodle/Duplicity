using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day6EndPoint : MonoBehaviour
{
    [SerializeField] private float waitTime = 2f;
    public GameObject joystick;

    void Start()
    {
        UIManager.Instance.DeactivateMapIcon();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //collision.gameObject.GetComponentInParent<VariableJoystick>().enabled = false;
            //collision.GetComponentInChildren<MoveController>().enabled = false;
            joystick.gameObject.SetActive(false);
            // ���̵� �ƿ� ����
            FadeManager.Instance.StartFadeOut(() =>
            {
                // ���̵� �ƿ��� �Ϸ�� �� ���� �۾� ����
                GameManager.Instance.GetCurrentDayController().CompleteTask("EnterRegenRepo");
            },true, 4f);
            StartCoroutine(WaitAndNextDay());
        }
    }

    private IEnumerator WaitAndNextDay()
    {
        yield return new WaitForSeconds(waitTime);
        GameManager.Instance.CompleteTask("RegenMainScene");
    }
}
