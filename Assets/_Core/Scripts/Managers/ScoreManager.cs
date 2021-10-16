using KnifeGame.Knife;
using KnifeGame.Util;
using System;
using UnityEngine;

namespace KnifeGame.Managers
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        public event Action<int> OnScoreChanged;
        public event Action<int> OnBestScoreChanged;
        public event Action<KnifeController, int> OnKnifeFlip;
        public event Action<KnifeController, int> OnKnifeHit;
        public event Action<KnifeController> OnKnifeMiss;

        private int _bestScore;
        private int _currentScore;

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

        private void Start()
        {
            
        }

        public void KnifeHit(KnifeController knife, int flips)
        {
            CurrentScore += flips + 1;
            if (CurrentScore > BestScore)
            {
                BestScore = CurrentScore;
            }
            OnKnifeHit?.Invoke(knife, flips);
        }

        public void KnifeMiss(KnifeController knife)
        {
            CurrentScore = 0;
            OnKnifeMiss?.Invoke(knife);
        }

        public void KnifeFlip(KnifeController knife, int points = 1)
        {
            OnKnifeFlip?.Invoke(knife, points);
        }
    }
}
