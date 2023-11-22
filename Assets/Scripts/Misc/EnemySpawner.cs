namespace Game
{
    using UnityEngine;
    using System.Collections;
    using Game.Health;
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] _prefabs;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _minHealth;
        [SerializeField] private float _delay;
        [SerializeField] private int _maxEnemyCount;
        private int _currentEnemyCount;
        public bool IsEmpty { get; private set; }
        private void Start()
        {
            _currentEnemyCount = 0;
            StartCoroutine(SpawnEnemy());
        }
        private void OnValidate()
        {
            if (_maxHealth < 0)
                _maxHealth = 0;
            if (_minHealth < 0)
                _minHealth = 0;
            if (_minHealth > _maxHealth)
                _minHealth = _maxHealth;
            if (_delay < 0)
                _delay = 0;
            if (_maxEnemyCount < 1)
                _maxEnemyCount = 1;
        }
        private IEnumerator SpawnEnemy()
        {
            if (_currentEnemyCount < _maxEnemyCount)
            {
                GameObject prefab = _prefabs[Random.Range(0, _prefabs.Length)];
                Transform point = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
                GameObject newEnemy = Instantiate(prefab, point.position, Quaternion.identity);
                newEnemy.transform.parent = transform;
                int randomHealth = (int)Random.Range(_minHealth, _maxHealth);
                HealthController health = newEnemy.GetComponentInChildren<HealthController>();
                health.RemoveContainer(100);
                for (int i = 1; i <= randomHealth; i++)
                {
                    health.AddContainer(1f);
                    health.AddHealth(1f);
                }
                _currentEnemyCount++;
                if (_currentEnemyCount >= _maxEnemyCount)
                    IsEmpty = true;
                yield return new WaitForSeconds(_delay);
                StartCoroutine(SpawnEnemy());
            }
        }
    }
}