using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(NewGameCreator))]
public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Image _loadBar;
    [SerializeField] private TMP_Text _loadText;
    [SerializeField] private float _delayBeforeLoading;

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _deleteProgressButton;
    [SerializeField] private Button _yesDeleteProgressButton;
    [SerializeField] private Button _noDeleteProgressButton;
    [SerializeField] private GameObject _confirmationPanel;

    [SerializeField] private AudioSource _buttonClickSound;

    [SerializeField] private TMP_Text _currentLevelInfo;
    [SerializeField] private TMP_Text _currentMoneyInfo;
    [SerializeField] private GameObject _gameProgressInfo;

    private int currentSceneId;
    private NewGameCreator newGameCreator;
    public PlayerData playerData;

    private AsyncOperation loading;

    private void Awake()
    {
        _loadBar.fillAmount = 0;
        _loadText.text = string.Empty;
        _shopButton.interactable = false;

        if (PlayerPrefs.HasKey("SaveGame"))
        {
            playerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("SaveGame"));
            currentSceneId = playerData.currentLevel;

            int currentMoney = playerData.money;
            SetGameProgressInfo(currentSceneId - 1, currentMoney);

            _shopButton.interactable = true;
            _deleteProgressButton.gameObject.SetActive(true);
        }

        newGameCreator = GetComponent<NewGameCreator>();
    }

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnStartGame);
        _shopButton.onClick.AddListener(OnShop);
        _exitButton.onClick.AddListener(OnExit);
        _deleteProgressButton.onClick.AddListener(OnDeleteProgressButtonClick);
        _yesDeleteProgressButton.onClick.AddListener(OnYesDeleteProgress);
        _noDeleteProgressButton.onClick.AddListener(OnNoDeleteProgress);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnStartGame);
        _shopButton.onClick.RemoveListener(OnShop);
        _exitButton.onClick.RemoveListener(OnExit);
        _deleteProgressButton.onClick.RemoveListener(OnDeleteProgressButtonClick);
        _yesDeleteProgressButton.onClick.RemoveListener(OnYesDeleteProgress);
        _noDeleteProgressButton.onClick.RemoveListener(OnNoDeleteProgress);
    }

    public void OnStartGame()
    {
        if (!PlayerPrefs.HasKey("SaveGame"))
        {
            newGameCreator.CreateNewGame();
            currentSceneId = 2;
        }
        else if (playerData.isInShop)
            currentSceneId = 1;

        _playButton.interactable = false;
        _shopButton.interactable = false;
        _exitButton.interactable = false;
        _deleteProgressButton.interactable = false;
        _buttonClickSound.Play();

        StartCoroutine(LoadSceneCoroutine(currentSceneId));
    }

    private void OnShop()
    {
        _playButton.interactable = false;
        _shopButton.interactable = false;
        _exitButton.interactable = false;
        _deleteProgressButton.interactable = false;
        _buttonClickSound.Play();

        StartCoroutine(LoadSceneCoroutine(1));
    }

    private void OnExit()
    {
        _exitButton.interactable = false;
        _shopButton.interactable = false;
        _playButton.interactable = false;
        _deleteProgressButton.interactable = false;
        _buttonClickSound.Play();

        Application.Quit();
    }

    private void OnDeleteProgressButtonClick()
    {
        _deleteProgressButton.interactable = false;
        _shopButton.interactable = false;
        _exitButton.interactable = false;
        _playButton.interactable = false;
        _buttonClickSound.Play();

        _confirmationPanel.SetActive(true);
    }

    private void OnYesDeleteProgress()
    {
        _buttonClickSound.Play();
        _confirmationPanel.SetActive(false);

        PlayerPrefs.DeleteKey("SaveGame");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnNoDeleteProgress()
    {
        _buttonClickSound.Play();

        _confirmationPanel.SetActive(false);

        _deleteProgressButton.interactable = true;
        _exitButton.interactable = true;
        _shopButton.interactable = true;
        _playButton.interactable = true;
    }

    private void SetGameProgressInfo(int currentLevel, int currentMoney)
    {
        _gameProgressInfo.SetActive(true);
        _currentLevelInfo.text = "Current Level: " + currentLevel.ToString();
        _currentMoneyInfo.text = "Current plasma: " + currentMoney.ToString();
    }

    private IEnumerator LoadSceneCoroutine(int sceneID)
    {
        yield return new WaitForSeconds(_delayBeforeLoading);

        loading = SceneManager.LoadSceneAsync(sceneID);

        while(!loading.isDone)
        {
            float progress = loading.progress / 0.9f;

            _loadBar.fillAmount = progress;
            _loadText.text = string.Format("Loading " + "{0:0}%", progress * 100f);

            yield return null;
        }
    }
}
