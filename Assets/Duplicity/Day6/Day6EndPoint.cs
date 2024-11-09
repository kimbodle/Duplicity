using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day6EndPoint : MonoBehaviour
{
    private FadeManager fadeManager;
    [SerializeField] private int waitTime;
    public GameObject joystick;

    void Start()
    {
        fadeManager = GetComponent<FadeManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //¿Ö¾ÈµÅ.
            //collision.gameObject.GetComponentInParent<VariableJoystick>().enabled = false;
            //collision.GetComponentInChildren<MoveController>().enabled = false;
            joystick.gameObject.SetActive(false);
            fadeManager.StartFadeIn();
            GameManager.Instance.GetCurrentDayController().CompleteTask("EnterRegenRepo");
            StartCoroutine(WaitAndNextDay());
        }
    }

    private IEnumerator WaitAndNextDay()
    {
        yield return new WaitForSeconds(waitTime);
        GameManager.Instance.CompleteTask("RegenScene");

    }
}
