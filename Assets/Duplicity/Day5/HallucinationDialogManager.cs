using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class HallucinationDialogManager : MonoBehaviour
{
    public GameObject hallucinationPanel; // 환청 다이얼로그 패널
    public TMP_Text hallucinationText; // 환청 다이얼로그 텍스트
    [SerializeField] float typingSpeed = 0.05f; // 타이핑 속도 조절 변수
    [SerializeField] private float displayDuration = 1f; // 글씨가 전부 뜬 후 유지되는 시간

    private bool isTyping = false; // 현재 타이핑 중인지 여부

    public event Action OnHallucinationDialogEnd; // 환청 종료 시 발생하는 이벤트

    private void Start()
    {
        hallucinationPanel.SetActive(false);
    }

    // 환청 다이얼로그 시작 메서드
    public void StartHallucinationDialog(Dialog hallucinationDialog)
    {
        if (isTyping) return;

        hallucinationPanel.SetActive(true);
        StartCoroutine(TypeSentence(hallucinationDialog.sentences[0]));
    }

    // 문장을 한 글자씩 표시한 후, typingSpeed 뒤에 패널을 비활성화
    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        hallucinationText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            hallucinationText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        EndHallucinationDialog();
        // 모든 글씨가 다 뜬 후 대사 유지
        yield return new WaitForSeconds(displayDuration);
    }

    // 환청 다이얼로그 종료 메서드
    public void EndHallucinationDialog()
    {
        hallucinationPanel.SetActive(false);
        hallucinationText.text = "";

        // 종료 이벤트 발생
        OnHallucinationDialogEnd?.Invoke();
    }
}
