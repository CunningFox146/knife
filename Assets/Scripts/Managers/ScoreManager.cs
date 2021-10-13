using KnifeGame.Util;
using System;
using UnityEngine;

namespace KnifeGame.Managers
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        public event Action<int> OnScoreChanged;
        public event Action<int> OnBestScoreChanged;
        public event Action<GameObject, int> OnKnifeFlip;

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

        public void KnifeHit(int flips)
        {
            CurrentScore += ++flips;
            if (CurrentScore > BestScore)
            {
                BestScore = CurrentScore;
            }
        }

        public void KnifeMiss()
        {
            CurrentScore = 0;
        }

        public void KnifeFlip(GameObject knife, int points = 1)
        {
            OnKnifeFlip?.Invoke(knife, points);
        }
    }
}
