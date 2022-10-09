using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TMP_Text))]
public class WaveNumberViewer : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    [Inject] private IEnemySpawner _enemySpawner;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _enemySpawner.WaveChanged += OnAllEnemySpawned;
    }

    private void OnDisable()
    {
        _enemySpawner.WaveChanged -= OnAllEnemySpawned;
    }

    private void OnAllEnemySpawned(int currentWave, int wavesCount)
    {
        _animator.SetTrigger("isWaveChanged");
        _text.text = "Wave " + currentWave.ToString() + " / " + wavesCount.ToString();
    }
}
