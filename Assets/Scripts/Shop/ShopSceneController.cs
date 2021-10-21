using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopSceneController : MonoBehaviour
{
    public static PlayerData PlayerData;

    [SerializeField] private TMP_Text _currentMoneyText;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _resetButton;
    [SerializeField] private float _delayBeforeLoading;
    [SerializeField] private Image _loadBar;
    [SerializeField] private TMP_Text _loadText;
    [SerializeField] private AudioSource _buttonClickSound;

    private AsyncOperation loading;

    private void Awake()
    {
        PlayerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("SaveGame"));
    }

    private void OnEnable()
    {
        _currentMoneyText.text = PlayerData.money.ToString();

        _loadBar.fillAmount = 0;
        _loadText.text = string.Empty;

        _continueButton.onClick.AddListener(OnNextLevel);
        _exitButton.onClick.AddListener(OnExitMainMenu);
        _resetButton.onClick.AddListener(OnResetPurchases);
    }

    private void OnDisable()
    {
        _continueButton.onClick.RemoveListener(OnNextLevel);
        _exitButton.onClick.RemoveListener(OnExitMainMenu);
        _resetButton.onClick.RemoveListener(OnResetPurchases);
    }

    private void OnNextLevel()
    {
        _continueButton.interactable = false;
        _exitButton.interactable = false;
        _resetButton.interactable = false;

        _buttonClickSound.Play();

        PlayerData.isInShop = false;
        PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(PlayerData));

        StartCoroutine(NextLevel());
    }

    private IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(_delayBeforeLoading);

        loading = SceneManager.LoadSceneAsync(PlayerData.currentLevel);

        while(!loading.isDone)
        {
            float progress = loading.progress / 0.9f;

            _loadBar.fillAmount = progress;
            _loadText.text = string.Format("Loading " + "{0:0}%", progress * 100f);

            yield return null;
        }
    }

    private void OnExitMainMenu()
    {
        _exitButton.interactable = false;
        _resetButton.interactable = false;
        _continueButton.interactable = false;

        _buttonClickSound.Play();

        PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(PlayerData));

        SceneManager.LoadScene(0);
    }

    private void OnResetPurchases()
    {
        _resetButton.interactable = false;
        _exitButton.interactable = false;
        _continueButton.interactable = false;

        _buttonClickSound.Play();

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
