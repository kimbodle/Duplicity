using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance; // 싱글톤 인스턴스

    public GameObject dialogPanel; // 다이얼로그 패널
    public TMP_Text dialogText; // 다이얼로그 텍스트
    public Image characterImage; // 캐릭터 이미지
    public Button nextButton; // 다음 문장 버튼
    public float typingSpeed = 0.05f; // 타이핑 속도 조절 변수

    [Space(10)]
    public Sprite playerImage; // 플레이어 이미지

    private Queue<(string sentence, bool isPlayerSpeaking)> sentenceQueue; // 화자 정보를 포함한 문장 큐

    // 다이얼로그 종료 이벤트
    public event Action OnDialogEnd;

    private void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스는 파괴
        }
    }

    private void Start()
    {
        sentenceQueue = new Queue<(string, bool)>();
        dialogPanel.SetActive(false);
        nextButton.onClick.AddListener(DisplayNextSentence); // 버튼 클릭 이벤트 추가
        nextButton.gameObject.SetActive(false); // 버튼을 처음에는 비활성화
    }

    public void StartDialog(Dialog dialog, Sprite characterSprite)
    {
        dialogPanel.SetActive(true);
        sentenceQueue.Clear();
        characterImage.sprite = characterSprite; // 기본 캐릭터 이미지 설정

        for (int i = 0; i < dialog.sentences.Length; i++)
        {
            // 문장과 화자 정보를 큐에 삽입
            bool isPlayerSpeaking = i < dialog.isPlayerSpeaking.Length && dialog.isPlayerSpeaking[i];
            sentenceQueue.Enqueue((dialog.sentences[i], isPlayerSpeaking));
        }

        nextButton.gameObject.SetActive(false); // 버튼 비활성화
        DisplayNextSentence(); // 첫 문장 표시
    }

    public void DisplayNextSentence()
    {
        if (sentenceQueue.Count == 0)
        {
            EndDialog();
            return;
        }

        (string sentence, bool isPlayerSpeaking) = sentenceQueue.Dequeue();

        // 화자에 따라 캐릭터 이미지를 설정
        characterImage.sprite = isPlayerSpeaking ? playerImage : characterImage.sprite;

        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // 타이핑 속도에 따라 대기
        }

        nextButton.gameObject.SetActive(true); // 문장이 끝나면 버튼 활성화
    }

    private void EndDialog()
    {
        dialogPanel.SetActive(false);
        nextButton.gameObject.SetActive(false); // 다이얼로그 종료 후 버튼 비활성화

        // 다이얼로그 종료 이벤트 호출
        OnDialogEnd?.Invoke();
    }

    // Player 혼잣말 메서드
    public void PlayerMessageDialog(Dialog dialog)
    {
        StartDialog(dialog, playerImage);
    }

    // DayController에서 제한에 따라 메세지 호출
    public void AdviseMessageDialog(int adviseMessageDialogNumber)
    {
        if (adviseMessageDialogNumber == 0)
        {
            StartDialog(new Dialog { sentences = new[] { "피난묘들과 조금 더 대화를 나눠보자." } }, playerImage);
        }
        else
        {
            StartDialog(new Dialog { sentences = new[] { "아직 여기서 해야 할 일이 더 남았어! 차근차근 꼼꼼히 조사해보자." } }, playerImage);
        }
    }
}
