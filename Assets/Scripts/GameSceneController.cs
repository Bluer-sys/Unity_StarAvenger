using System;
using System.Collections;
using DefaultNamespace.UI;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class GameSceneController : MonoBehaviour
{
    public static PlayerData PlayerData;

    [SerializeField] private Player         _player;
    
    [Inject] private IUiView        _uiView;
    [Inject] private IEnemySpawner  _enemySpawner;

    private int currentLevelID;

    private void Awake()
    {
        PlayerData      = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("SaveGame"));

        _uiView.SetCompleteLevelTextAlpha( 0 );
        currentLevelID  = SceneManager.GetActiveScene().buildIndex;
        
        SubscribeClick( _uiView.MainMenuButton, OnMainMenuButtonClick );
        SubscribeClick( _uiView.RestartButton,  OnRestartLevelButtonClick );
        SubscribeClick( _uiView.ExitButton,     OnExitButtonClick );
        SubscribeClick( _uiView.ContinueButton, OnContinueButtonClick );
    }

    private void OnEnable()
    {
        _player.PlayerDied                      += OnPlayerDie;
        _enemySpawner.AllWavesSpawned           += OnChangeLevel;
    }

    private void OnDisable()
    {
        _player.PlayerDied                      -= OnPlayerDie;
        _enemySpawner.AllWavesSpawned           -= OnChangeLevel;
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
        _uiView.SetGameOverTextActive( true );

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
        _uiView.CompleteLevelAnimator.SetBool(IsLevelComplete, true);
        _uiView.SetCompleteLevelTextAlpha( 1 );
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

    void SubscribeClick(Button button, Action action)
    {
        button.OnClickAsObservable()
            .Subscribe( _ => action() )
            .AddTo( this );
    }

    private void OnMainMenuButtonClick()
    {
        _uiView.PlayButtonClickSound();
        _uiView.SetAllButtonsInteractable( false );
        
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void OnRestartLevelButtonClick()
    {
        _uiView.PlayButtonClickSound();
        _uiView.SetAllButtonsInteractable( false );

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    private void OnExitButtonClick()
    {
        _uiView.PlayButtonClickSound();

        _uiView.SetMenuActive(false);

        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    private void OnContinueButtonClick()
    {
        _uiView.PlayButtonClickSound();

        _uiView.SetMenuActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }
    
    // Constants
    static readonly int IsLevelComplete     = Animator.StringToHash( "isLevelComplete" );
}
