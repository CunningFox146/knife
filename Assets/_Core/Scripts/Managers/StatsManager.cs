using KnifeGame.Knife;
using KnifeGame.Managers.ModeManagers;
using KnifeGame.Util;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KnifeGame.Managers
{
    public class StatsManager : Singleton<StatsManager>
    {
        public event Action<int> OnCoinsChanged;

        public event Action<int> OnScoreChanged;
        public event Action<int> OnBestScoreChanged;

        public event Action<KnifeController, int> OnKnifeFlip;
        public event Action<KnifeController, int> OnKnifeHit;
        public event Action<KnifeController> OnKnifeMiss;

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
                SaveManager.CurrentSave.highScore[ScenesManager.Inst.Mode] = value;
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
                SaveManager.CurrentSave.coins = value;
            }
        }

        public void ResetScore() => CurrentScore = 0;

        private void Start()
        {
            CoinsCount = SaveManager.CurrentSave.coins;
            if (SaveManager.CurrentSave.highScore.TryGetValue(ScenesManager.Inst.Mode, out int score))
            {
                BestScore = score;
            }
        }

        public void KnifeHit(KnifeController knife, int flips)
        {
            OnKnifeHit?.Invoke(knife, flips);
        }

        public void KnifeMiss(KnifeController knife)
        {
            OnKnifeMiss?.Invoke(knife);
        }

        public void KnifeFlip(KnifeController knife, int points = 1)
        {
            OnKnifeFlip?.Invoke(knife, points);
        }
    }
}
