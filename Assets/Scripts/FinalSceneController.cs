using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalSceneController : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private AudioSource _buttonClickSound;

    private PlayerData playerData;

    private void Awake()
    {
        SetPreviousLevel();
    }

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExit);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExit);
    }

    private void SetPreviousLevel()
    {
        playerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("SaveGame"));
        playerData.currentLevel = SceneManager.GetActiveScene().buildIndex - 1;
        PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(playerData));

    }

    private void OnExit()
    {
        _buttonClickSound.Play();

        _exitButton.interactable = false;

        SceneManager.LoadScene(0);
    }
}
