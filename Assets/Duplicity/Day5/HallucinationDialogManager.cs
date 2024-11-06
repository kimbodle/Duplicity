using System.Collections;
using TMPro;
using UnityEngine;

public class HallucinationDialogManager : MonoBehaviour
{
    public GameObject hallucinationPanel; // 환청 다이얼로그 패널
    public TMP_Text hallucinationText; // 환청 다이얼로그 텍스트
    [SerializeField] float typingSpeed = 0.05f; // 타이핑 속도 조절 변수
    [SerializeField] private float displayDuration = 2f; // 글씨가 전부 뜬 후 유지되는 시간 (2초)

    private bool isTyping = false; // 현재 타이핑 중인지 여부

    private void Start()
    {
        hallucinationPanel.SetActive(false); // 초기에는 환청 패널을 비활성화
    }

    // 환청 다이얼로그 시작 메서드
    public void StartHallucinationDialog(Dialog hallucinationDialog)
    {
        if (isTyping) return;

        hallucinationPanel.SetActive(true);
        StartCoroutine(TypeSentence(hallucinationDialog.sentences[0]));
    }

    // 문장을 한 글자씩 표시한 후, 2초 뒤에 패널을 비활성화하는 코루틴
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

        // 모든 글씨가 다 뜬 후 대사를 2초간 유지
        yield return new WaitForSeconds(displayDuration);
        EndHallucinationDialog();
    }

    // 환청 다이얼로그 종료 메서드
    public void EndHallucinationDialog()
    {
        hallucinationPanel.SetActive(false);
        hallucinationText.text = "";
    }
}
