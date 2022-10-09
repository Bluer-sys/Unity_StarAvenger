using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class GameSceneController : MonoBehaviour
{
    public static PlayerData PlayerData;

    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private TMP_Text _completeLevelText;
    [SerializeField] private Animator _completeLevelAnimator;
    [SerializeField] private Button[] _gameButtons;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private AudioSource _buttonClickSound;
    
    [Inject] private IEnemySpawner _enemySpawner;

    private int currentLevelID;

    private void Awake()
    {
        PlayerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("SaveGame"));

        _completeLevelText.alpha = 0;
        currentLevelID = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnEnable()
    {
        _player.PlayerDied += OnPlayerDie;
        _enemySpawner.AllWavesSpawned += OnChangeLevel;
        _gameButtons[0].onClick.AddListener(OnMainMenuButtonClick);
        _gameButtons[1].onClick.AddListener(OnRestartLevelButtonClick);
        _gameButtons[2].onClick.AddListener(OnExitButtonClick);
        _gameButtons[3].onClick.AddListener(OnContinueButtonClick);
    }

    private void OnDisable()
    {
        _player.PlayerDied -= OnPlayerDie;
        _enemySpawner.AllWavesSpawned -= OnChangeLevel;
        _gameButtons[0].onClick.RemoveListener(OnMainMenuButtonClick);
        _gameButtons[1].onClick.RemoveListener(OnRestartLevelButtonClick);
        _gameButtons[2].onClick.RemoveListener(OnExitButtonClick);
        _gameButtons[3].onClick.RemoveListener(OnContinueButtonClick);
    }

    private void OnPlayerDie(int currentMoney)
    {
        PlayerData.money += Mathf.Abs(currentMoney - PlayerData.money) / 2;
        PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(PlayerData));

        StartCoroutine(SetGameOver());
    }

    private void OnChangeLevel()
    {
        PlayerData.money = _player.Money;
        PlayerData.isInShop = true;
        PlayerData.currentLevel = currentLevelID + 1;
        PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(PlayerData));

        StartCoroutine(TryGoToShop());
    }

    private IEnumerator SetGameOver()
    {
        yield return new WaitForSeconds(1.0f);
        _gameOverText.gameObject.SetActive(true);

        while(Time.timeScale > 0.1f)
        {
            Time.timeScale -= 0.001f;
            yield return null;
        }

        Time.timeScale = 0;
    }

    private IEnumerator TryGoToShop()
    {
        yield return new WaitForSeconds(2.0f);
        _completeLevelAnimator.SetBool("isLevelComplete", true);
        _completeLevelText.alpha = 1;
        yield return new WaitForSeconds(5.0f);

        if (SceneManager.sceneCountInBuildSettings - 1 != SceneManager.GetActiveScene().buildIndex + 1)
        {
            SceneManager.LoadSceneAsync(1);
        }
        else
        {
            SceneManager.LoadSceneAsync(SceneManager.sceneCountInBuildSettings - 1);
        }
    }

    private void OnMainMenuButtonClick()
    {
        _buttonClickSound.Play();

        for (int i = 0; i < 2; i++)
        {
            _gameButtons[i].interactable = false;
        }

        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void OnRestartLevelButtonClick()
    {
        _buttonClickSound.Play();

        for (int i = 0; i < 2; i++)
        {
            _gameButtons[i].interactable = false;
        }

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    private void OnExitButtonClick()
    {
        _buttonClickSound.Play();

        _menuPanel.SetActive(false);

        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    private void OnContinueButtonClick()
    {
        _buttonClickSound.Play();

        _menuPanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }
}
