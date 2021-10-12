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
            ScoreManager.Inst.OnBestScoreChanged += OnScoreChangedHandler;
            OnScoreChangedHandler(ScoreManager.Inst.BestScore);
        }

        private void OnScoreChangedHandler(int score)
        {
            _text.text = score.ToString();
        }
    }
}
