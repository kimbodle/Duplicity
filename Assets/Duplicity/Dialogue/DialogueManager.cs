using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance; // 싱글톤 인스턴스

    [Header("UI Components")]
    public GameObject dialogPanel;            // 대화 패널
    public TMP_Text dialogText;               // 현재 대사 텍스트
    public Image characterImage;              // 캐릭터 이미지
    public Button nextButton;                 // 다음 버튼
    public float typingSpeed = 0.05f; // 타이핑 속도 조절 변수
    public Dialog[] advissMessages;
    [Header("History UI")]
    public GameObject historyPanel;           // 히스토리 패널
    public Transform contentParent;           // ScrollView Content
    public GameObject dialogEntryPrefab;      // 프리팹
    [Header("Assets")]
    public Sprite lauraImage;

    private Queue<(Dialog dialog, Sprite characterSprite)> dialogQueue; // 대기 중인 다이얼로그 큐
    private Queue<(string sentence, bool isPlayerSpeaking)> sentenceQueue; // 화자 정보를 포함한 문장 큐
    private List<Dialog> dialogHistory;       // 히스토리에 저장되는 Dialog
    private bool isTyping = false; // 코루틴 실행 여부를 나타내는 변수
    private bool isDialogActive = false; // 다이얼로그 활성 상태를 추적하는 변수
    private Sprite currentCharacterSprite; // 현재 대화 중인 캐릭터 이미지
    private string currentSentence; // 현재 문장
    private bool currentIsPlayerSpeaking; // 현재 화자 정보


    // 다이얼로그 종료 이벤트
    public event Action OnDialogEnd;
    //스킵 기능 온오프
    //public bool allowSkip = true;

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
        dialogQueue = new Queue<(Dialog, Sprite)>();
        sentenceQueue = new Queue<(string, bool)>();
        dialogHistory = new List<Dialog>();
        dialogPanel.SetActive(false);
        nextButton.onClick.AddListener(DisplayNextSentence); // 버튼 클릭 이벤트 추가
        nextButton.gameObject.SetActive(false); // 버튼을 처음에는 비활성화
    }

    public void StartDialog(Dialog dialog, Sprite characterSprite)
    {
        // 다이얼로그가 진행 중이면 큐에 저장
        if (isDialogActive || isTyping)
        {
            dialogQueue.Enqueue((dialog, characterSprite));
            return;
        }

        // 히스토리에 Dialog 저장 (중복 방지)
        if (!dialogHistory.Contains(dialog))
        {
            dialogHistory.Add(dialog);
        }

        // 다이얼로그 시작
        isDialogActive = true;
        dialogPanel.SetActive(true);
        currentCharacterSprite = characterSprite; // 현재 캐릭터 이미지를 저장
        characterImage.sprite = characterSprite; // 다이얼로그 시작 시 캐릭터 이미지 설정
        sentenceQueue.Clear();

        for (int i = 0; i < dialog.sentences.Length; i++)
        {
            bool isPlayerSpeaking = i < dialog.isPlayerSpeaking.Length && dialog.isPlayerSpeaking[i];
            sentenceQueue.Enqueue((dialog.sentences[i], isPlayerSpeaking));
        }

        nextButton.gameObject.SetActive(false); // 버튼 비활성화
        DisplayNextSentence(); // 첫 문장 표시
    }

    public void DisplayNextSentence()
    {
        // 코루틴이 실행 중이면 클릭이 무시됨
        if (isTyping)
        {
            return;
        }

        if (sentenceQueue.Count == 0)
        {
            EndDialog();
            return;
        }

        // 다음 문장을 큐에서 가져오고 현재 문장 변수에 저장
        (currentSentence, currentIsPlayerSpeaking) = sentenceQueue.Dequeue();

        // 화자에 따라 캐릭터 이미지 설정
        characterImage.sprite = currentIsPlayerSpeaking ? lauraImage : currentCharacterSprite;

        // 문장 타이핑 시작
        StartCoroutine(TypeSentence(currentSentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true; // 코루틴 시작 시 true로 설정
        dialogText.text = "";
        nextButton.gameObject.SetActive(false); // 타이핑 중에는 버튼 비활성화

        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // 타이핑 속도에 따라 대기
        }
        isTyping = false; // 코루틴 종료 시 false로 설정
        nextButton.gameObject.SetActive(true); // 문장이 끝나면 버튼 활성화
    }

    private void EndDialog()
    {
        dialogPanel.SetActive(false);
        nextButton.gameObject.SetActive(false); // 다이얼로그 종료 후 버튼 비활성화
        isTyping = false; // 다이얼로그 종료 시에도 false로 설정
        isDialogActive = false; // 다이얼로그가 종료되면 비활성 상태로 설정

        // 다이얼로그 종료 이벤트 호출
        OnDialogEnd?.Invoke();

        // 대기 중인 다이얼로그가 있으면 자동으로 시작
        if (dialogQueue.Count > 0)
        {
            (Dialog nextDialog, Sprite nextSprite) = dialogQueue.Dequeue();
            StartDialog(nextDialog, nextSprite);
        }
    }

    public void UpdateHistoryUI()
    {
        // 기존 UI 제거
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // 히스토리 UI 추가
        foreach (var dialog in dialogHistory)
        {
            for (int i = 0; i < dialog.sentences.Length; i++)
            {
                GameObject dialogEntry = Instantiate(dialogEntryPrefab, contentParent);

                Image characterImage = dialogEntry.transform.Find("CharactorImage").GetComponent<Image>();
                TMP_Text dialogText = dialogEntry.transform.Find("DialogHistoryText").GetComponent<TMP_Text>();

                // 플레이어 또는 NPC 이미지 설정
                bool isPlayerSpeaking = dialog.isPlayerSpeaking[i];
                characterImage.sprite = isPlayerSpeaking ? lauraImage : dialog.characterSprite;

                /*
                // 텍스트 설정
                dialogText.text = isPlayerSpeaking
                    ? $"<color=blue>Player:</color> {dialog.sentences[i]}"
                    : $"<color=green>NPC:</color> {dialog.sentences[i]}";
                */
                dialogText.text = dialog.sentences[i];
            }
        }
    }

    public void ToggleHistoryUI()
    {
        historyPanel.SetActive(!historyPanel.activeSelf);

        if (historyPanel.activeSelf)
        {
            UpdateHistoryUI();
        }
    }

    public void ClearHistory()
    {
        // 히스토리 리스트 비우기
        dialogHistory.Clear();

        // 히스토리 UI 초기화
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        Debug.Log("대화 히스토리가 초기화");
    }

    public void SkipDialog()
    {
        if (isTyping)
        {
            // 코루틴 중단
            StopAllCoroutines();

            // 현재 문장을 즉시 완성
            dialogText.text = currentSentence;

            // 상태 초기화
            isTyping = false; // 타이핑 상태 해제
            nextButton.gameObject.SetActive(true); // 다음 버튼 활성화
        }
    }



    /*
    public void SkipDialog()
    {
        //if (!allowSkip) return;
        if (isTyping)
        {
            // 타이핑 즉시 완료
            StopAllCoroutines();
            dialogText.text = sentenceQueue.Peek().sentence; // 현재 문장 완성
            isTyping = false;
            nextButton.gameObject.SetActive(true); // 버튼 활성화
        }
        /*
        else if (sentenceQueue.Count > 0)
        {
            // 다음 문장으로 이동
            DisplayNextSentence();
        }
        else
        {
            // 대화 종료
            EndDialog();
        }
    }
*/


    public void PlayerMessageDialog(Dialog dialog)
    {
        StartDialog(dialog, lauraImage);
    }

    public void AdviseMessageDialog(int adviseMessageDialogNumber)
    {
        if (adviseMessageDialogNumber < advissMessages.Length)
        {
            StartDialog(advissMessages[adviseMessageDialogNumber], lauraImage);
        }
        else
        {
            Debug.LogWarning("유효하지 않은 조언 메시지 인덱스");
        }
    }
}
