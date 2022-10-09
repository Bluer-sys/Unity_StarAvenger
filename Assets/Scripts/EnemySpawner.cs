using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEnemySpawner
{
    void OnEnemyDied(Enemy enemy);
    void OnUselessEnemyDied(Enemy enemy);
    UnityAction AllWavesSpawned { get; set; }
    UnityAction<int, int> WaveChanged { get; set; }
}

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    [SerializeField] private Transform[] _spawnPointsForRandom;
    [SerializeField] private List<Wave> _waves;

    private int alredyKilled = 0;
    private int currentWaveNumber = 0;
    private Wave currentWave;

    public UnityAction AllWavesSpawned { get; set; }
    public UnityAction<int, int> WaveChanged { get; set; }

    private void Awake()
    {
        currentWave = _waves[currentWaveNumber];
    }

    private void Start()
    {
        WaveChanged?.Invoke(currentWaveNumber, _waves.Count);

        StartCoroutine(SpawnWithDelay());
    }

    private IEnumerator SpawnWithDelay()
    {
        yield return new WaitForSeconds(currentWave.DelayBeforeWave);
        WaveChanged?.Invoke(currentWaveNumber + 1, _waves.Count);

        int sumEnemyCountInWave = 0;
        for (int i = 0; i < currentWave._waveParts.Length; i++)
        {
            sumEnemyCountInWave += currentWave._waveParts[i].EnemyCount;
        }

        for (int currentPartNumber = 0; currentPartNumber < currentWave._waveParts.Length; currentPartNumber++)
        {
            for (int i = 0; i < currentWave._waveParts[currentPartNumber].EnemyCount; i++) 
            {
                yield return new WaitForSeconds(currentWave._waveParts[currentPartNumber].DelayBeforeEnemy);

                if (currentWave._waveParts[currentPartNumber].EnemyType.TryGetEnemyInPool(out Enemy enemy))
                {
                    Transform spawnPoint;

                    if (!currentWave._waveParts[currentPartNumber].isRandom)
                        spawnPoint = currentWave._waveParts[currentPartNumber].SpawnPoint;
                    else spawnPoint = _spawnPointsForRandom[Random.Range(0, _spawnPointsForRandom.Length - 1)];

                    enemy.gameObject.SetActive(true);
                    enemy.transform.position = spawnPoint.position;

                    enemy.EnemyDied += OnEnemyDied;
                    enemy.EnemyOutLifeZone += OnEnemyDied;
                }
            }
        }

        yield return new WaitWhile(() => alredyKilled < sumEnemyCountInWave);
        NextWave();
    }

    private void NextWave()
    {
        currentWaveNumber++;

        if (currentWaveNumber > _waves.Count - 1)
        {
            AllWavesSpawned?.Invoke();
            Debug.Log("FINISH");
            return;
        }

        currentWave = _waves[currentWaveNumber];
        alredyKilled = 0;

        StartCoroutine(SpawnWithDelay());
    }

    public void OnEnemyDied(Enemy enemy)
    {
        alredyKilled++;

        enemy.EnemyDied -= OnEnemyDied;
        enemy.EnemyOutLifeZone -= OnEnemyDied;

        enemy.gameObject.SetActive(false);
    }

    public void OnUselessEnemyDied(Enemy enemy)
    {
        enemy.EnemyDied -= OnUselessEnemyDied;
        enemy.EnemyOutLifeZone -= OnUselessEnemyDied;

        enemy.transform.position = transform.position;
        enemy.gameObject.SetActive(false);
    }
}

[System.Serializable]
public class Wave
{
    [SerializeField] public float DelayBeforeWave;
    [SerializeField] public WavePart[] _waveParts;
}

[System.Serializable]
public class WavePart
{
    [SerializeField] public int EnemyCount;
    [SerializeField] public float DelayBeforeEnemy;
    [SerializeField] public EnemyPool EnemyType;
    [SerializeField] public Transform SpawnPoint;
    [SerializeField] public bool isRandom;
}
