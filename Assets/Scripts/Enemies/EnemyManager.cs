using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform _spawnParent;

    public List<Enemy> _enemyTypes = new List<Enemy>();

    public List<Transform> _spawnPoints = new List<Transform>();

    public int _desiredEnemies = 5;

    public float _spawnRate = 2.0f;

    private float _timeSinceLastSpawn = 0.0f;

    private bool _canSpawn = true;

    public static int _totalEnemies = 0;

    private void Update()
    {
        UpdateSpawnTimer();

        SpawnDesiredEnemies();
    }

    private void UpdateSpawnTimer()
    {
        if (!_canSpawn)
        {
            if (_timeSinceLastSpawn >= _spawnRate)
            {
                _canSpawn = true;
                _timeSinceLastSpawn = 0.0f;
            }

            _timeSinceLastSpawn += Time.deltaTime;
        }
    }

    private void SpawnDesiredEnemies()
    {
        if (_totalEnemies < _desiredEnemies)
        {
            if (_canSpawn)
            {
                SpawnRandomEnemy();

                _canSpawn = false;
            }
        }
    }

    private void SpawnRandomEnemy()
    {
        int enemyType = Random.Range(0, _enemyTypes.Count);

        int spawnPoint = Random.Range(0, _spawnPoints.Count);

        Instantiate(_enemyTypes[enemyType], _spawnPoints[spawnPoint].position, _spawnPoints[spawnPoint].rotation, _spawnParent);

        _totalEnemies++;
    }
}
