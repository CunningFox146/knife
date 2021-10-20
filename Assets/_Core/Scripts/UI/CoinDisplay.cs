using DG.Tweening;
using KnifeGame.Managers;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class CoinDisplay : MonoBehaviour
    {
        [SerializeField] private Text _text;

        private int _currentCoins = 0;

        private void Start()
        {
            ScoreManager.Inst.OnCoinsChanged += OnCoinsChangedHandler;

            OnCoinsChangedHandler(ScoreManager.Inst.CoinsCount);
        }

        private void OnCoinsChangedHandler(int coins)
        {
            _currentCoins = coins;
            _text.text = coins.ToString();
        }
    }
}
