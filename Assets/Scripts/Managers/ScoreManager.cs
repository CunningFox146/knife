using KnifeGame.Util;
using System;

namespace KnifeGame.Managers
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        public event Action<int> OnScoreChanged;
        public event Action<int> OnBestScoreChanged;

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

        public void KnifeHit()
        {
            CurrentScore++;
            if (CurrentScore > BestScore)
            {
                BestScore = CurrentScore;
            }
        }

        public void KnifeMiss()
        {
            CurrentScore = 0;
        }
    }
}
