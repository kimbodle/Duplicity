using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }
    [Header("Login")]
    public GameObject LoginUI;

    [Header("Setting")]
    public Button settingIcon;
    public GameObject settingUI;

    [Header("Map")]
    public GameObject mapUI; // 지도 UI 오브젝트
    public Button mapIconButton; // 지도 아이콘 버튼

    [Header("Inventory")]
    public GameObject inventoryManager;
    public Button inventoryIconButton;
    public GameObject inventoryUI;

    [Header("DialogHistory")]
    public GameObject dialogHistoryIconButton;

    [Header("Days")]
    public GameObject dayIntroCanvas;
    public Image dayIntroImage;
    public List<Sprite> daySprites; // Inspector에서 순서대로 추가: 0 -> Day 1, 1 -> Day 2, 등
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
        LoginUI.SetActive(false);
        mapUI.SetActive(false);

        DeactivateInventory();
        inventoryUI.SetActive(false);

        mapIconButton.gameObject.SetActive(false);
        dialogHistoryIconButton.gameObject.SetActive(false);

        //mapIconButton.onClick.AddListener(ToggleMapUI);
        InitializeDaySprites();
    }

    //Login
    public void OpenLoginUI()
    {
        LoginUI.SetActive(true);
    }

    public void CloseLoginUI()
    {
        LoginUI.SetActive(false);
    }

    //Setting
    public void ToggleSettingUI()
    {
        settingUI.SetActive(!settingUI.activeSelf);
    }

    public void LogoutButton()
    {
        FirebaseAuthController.Instance.Logout();
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }

    //Map
    public void ActiveMapIcon()
    {
        mapIconButton.gameObject.SetActive(true);
    }
    public void DeactivateMapIcon()
    {
        mapIconButton.gameObject.SetActive(false);
    }

    public void ToggleMapUI()
    {
        mapUI.SetActive(!mapUI.activeSelf);
    }

    //Inventory
    public void ActiveInventory()
    {
        Debug.Log("인벤토리 활성화");
        inventoryManager.SetActive(true);
        inventoryIconButton.gameObject.SetActive(true);
    }

    public void DeactivateInventory()
    {
        inventoryManager.SetActive(false);
        inventoryIconButton.gameObject.SetActive(false);
    }

    public void TogglInventoryUI()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }
    
    //DialogHistorySystem
    public void ActiveDialogHistoryIcon()
    {
        dialogHistoryIconButton.SetActive(true);
    }
    public void DeactivateDialogHistoryIcon()
    {
        dialogHistoryIconButton.SetActive(false);
    }

    public void TogglDialogHistoryUI()
    {
        DialogManager.Instance.ToggleHistoryUI();
    }

    //Ending
    public void EndingUI()
    {
        DeactivateInventory();
        DeactivateMapIcon();
        mapUI.SetActive(false);
    }

    //DayIntro
    private void InitializeDaySprites()
    {
        daySpriteDictionary = new Dictionary<int, Sprite>();

        for (int i = 0; i < daySprites.Count; i++)
        {
            daySpriteDictionary.Add(i + 1, daySprites[i]);
        }
    }
    public void DisplayDayIntro(int currentDay)
    {
        if (currentDay == 0) { return; }
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
