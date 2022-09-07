using KnifeGame.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeGame.UI.Displayers
{
    public class BestScoreDisplayer : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        private void Start()
        {
            StatsManager.Inst.OnBestScoreChanged += OnScoreChangedHandler;
            OnScoreChangedHandler(StatsManager.Inst.BestScore);
        }

        private void OnDestroy()
        {
            StatsManager.Inst.OnBestScoreChanged -= OnScoreChangedHandler;
        }

        private void OnScoreChangedHandler(int score)
        {
            _text.text = score.ToString();
        }
    }
}
