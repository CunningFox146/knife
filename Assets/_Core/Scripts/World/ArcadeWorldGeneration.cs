using KnifeGame.Knife;
using KnifeGame.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeGame.World
{
    public class ArcadeWorldGeneration : MonoBehaviour
    {
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private float _minX = -5f;
        [SerializeField] private float _maxX = 5f;
        [SerializeField] private float _chancePerPoint = 0.5f;
        [SerializeField] private float _spawnDistance = 10f;

        private float _lastY = 0f;

        private List<Transform> _coins;

        private void Awake()
        {
            _coins = new List<Transform>();
        }

        private void Start()
        {
            StatsManager.Inst.OnKnifeHit += OnKnifeHit;
            StatsManager.Inst.OnKnifeMiss += OnKnifeMiss;

            SpawnCoins();
        }

        private void SpawnCoins(float startY = 0f)
        {
            for (float y = 3f; y <= startY + _spawnDistance; y++)
            {
                SpawnCoin(y);
            }
        }

        private void SpawnCoin(float y)
        {
            var coin = Instantiate(_coinPrefab);
            coin.transform.position = new Vector3(Random.Range(_minX, _maxX), y, 0f);
            _coins.Add(coin.transform);
        }

        private void OnKnifeMiss(KnifeController knife)
        {
            foreach (Transform coin in _coins)
            {
                Destroy(coin.gameObject);
            }
            _coins = new List<Transform>();
        }

        private void OnKnifeHit(KnifeController knife, int flips)
        {
            float y = knife.transform.position.y;
            for (int i = 0; i < _coins.Count; i++)
            {
                Transform coin = _coins[i];
                if (y - coin.position.y >= 5f)
                {
                    Destroy(coin.gameObject);
                    _coins.RemoveAt(i);
                }
            }
            SpawnCoins(y);
        }
    }
}
