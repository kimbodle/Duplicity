using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }

    public GameObject LoginUI;

    [Space(10)] //지도
    public GameObject mapCanvas; // 지도 UI 오브젝트
    public Button mapIconButton; // 종이 아이콘 버튼


    [Space(10)] //인벤토리
    public GameObject inventoryCanvas; // 인벤토리 UI 패널
    //public InventoryUI inventoryUI;
    public Button inventoryUIButton;

    public GameObject dayIntroCanvas;
    public List<Sprite> daySprites; // Inspector에서 순서대로 추가: 0 -> Day 1, 1 -> Day 2, 등
    public Image dayIntroImage;
    private Dictionary<int, Sprite> daySpriteDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //inventoryUI = inventoryUI.GetComponent<InventoryUI>();
        /*
        inventoryCanvas.SetActive(false);
        LoginUI.SetActive(false);
        mapCanvas.SetActive(false);
        


        //onClick 연결
        mapIconButton.onClick.AddListener(ToggleMapUI);
        inventoryUIButton.onClick.AddListener(ToggleInventoryUI);
        */
        InitializeDaySprites();
    }

    public void OpenLoginUI()
    {
        LoginUI.SetActive(true);
    }

    public void CloseLoginUI()
    {
        LoginUI.SetActive(false);
    }
    public void ToggleMapUI()
    {
        mapCanvas.SetActive(!mapCanvas.activeSelf);
    }
    /*public void ToggleInventoryUI()
    {
        inventoryUI.ToggleInventory();
    }*/

    // 인벤토리 창을 열고 닫는 함수
    public void ToggleInventoryUI()
    {
        inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
    }

    private void InitializeDaySprites()
    {
        daySpriteDictionary = new Dictionary<int, Sprite>();

        for (int i = 0; i < daySprites.Count; i++)
        {
            daySpriteDictionary.Add(i+1, daySprites[i]);
        }
    }
    public void DisplayDayIntro(int currentDay)
    {
        if(currentDay == 0) { return; }
        if (daySpriteDictionary.TryGetValue(currentDay, out Sprite daySprite))
        {
            dayIntroCanvas.SetActive(true);
            dayIntroImage.sprite = daySprite;
            StartCoroutine(DisplayDayIntroCoroutine());
        }
        else
        {
            Debug.LogWarning($"이미지가 없는 Day: {currentDay}");
        }
    }

    private IEnumerator DisplayDayIntroCoroutine()
    {
        yield return new WaitForSeconds(3f); // 예시로 3초 대기
        dayIntroCanvas.SetActive(false);
    }
}
