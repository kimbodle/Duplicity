using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day6EndPoint : MonoBehaviour
{
    private FadeManager fadeManager;
    [SerializeField] private float waitTime = 2f;
    public GameObject joystick;

    void Start()
    {
        fadeManager = GetComponent<FadeManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //왜안돼.
            //collision.gameObject.GetComponentInParent<VariableJoystick>().enabled = false;
            //collision.GetComponentInChildren<MoveController>().enabled = false;
            joystick.gameObject.SetActive(false);
            fadeManager.OnlyStartFadeIn();
            /*
            // 페이드 아웃을 시작하고 완료 후 페이드 인
            fadeManager.StartFadeOut(() =>
            {
                // 페이드 아웃이 완료되면 페이드 인 시작
                fadeManager.StartFadeIn();
            });*/
            //colider로 막기
            GameManager.Instance.GetCurrentDayController().CompleteTask("EnterRegenRepo");
            StartCoroutine(WaitAndNextDay());
        }
    }

    private IEnumerator WaitAndNextDay()
    {
        yield return new WaitForSeconds(waitTime);
        GameManager.Instance.CompleteTask("ShelterScene");
    }
}
