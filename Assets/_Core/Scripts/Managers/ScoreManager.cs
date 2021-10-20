using KnifeGame.Knife;
using KnifeGame.Util;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KnifeGame.Managers
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        public event Action<int> OnCoinsChanged;

        public event Action<int> OnScoreChanged;
        public event Action<int> OnBestScoreChanged;

        public event Action<KnifeController, int> OnKnifeFlip;
        public event Action<KnifeController, int> OnKnifeHit;
        public event Action<KnifeController> OnKnifeMiss;

        [SerializeField] private GameObject _coinPrefab;

        private int _bestScore;
        private int _currentScore;
        private int _coinsCount;

        public int BestScore
        {
            get => _bestScore;
            set
            {
                _bestScore = value;
                OnBestScoreChanged?.Invoke(value);
            }
        }
        public int CurrentScore
        {
            get => _currentScore;
            set
            {
                _currentScore = value;
                OnScoreChanged?.Invoke(value);
            }
        }

        public int CoinsCount
        {
            get => _coinsCount;
            set
            {
                if (_coinsCount != value)
                {
                    OnCoinsChanged?.Invoke(value);
                }
                _coinsCount = value;
            }
        }

        public void KnifeHit(KnifeController knife, int flips)
        {
            CurrentScore += (flips + 1) * knife.info.perFlip;
            if (CurrentScore > BestScore)
            {
                BestScore = CurrentScore;
            }
            SpawnCoins(knife, flips);
            OnKnifeHit?.Invoke(knife, flips);
        }

        public void KnifeMiss(KnifeController knife)
        {
            ResetScore();
            OnKnifeMiss?.Invoke(knife);
        }

        public void ResetScore()
        {
            CurrentScore = 0;
        }

        public void KnifeFlip(KnifeController knife, int points = 1)
        {
            OnKnifeFlip?.Invoke(knife, points);
        }

        private void SpawnCoins(KnifeController knife, int points)
        {
            if (points <= 0) return;

            CoinsCount += points;

            float startAngle = -Mathf.PI * 0.5f;
            for (int i = 0; i < points; i++)
            {
                float angle = i / (float)points * Mathf.PI * 2 + startAngle;
                Vector3 direction = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));
                var coin = Instantiate(_coinPrefab);

                coin.transform.position = knife.transform.position + Vector3.up + direction * 0.5f;
                coin.GetComponent<Rigidbody>().AddForce(direction * Random.Range(2f, 3f), ForceMode.Impulse);
            }
        }
    }
}
