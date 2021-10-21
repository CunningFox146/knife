using KnifeGame.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class CoinDisplay : MonoBehaviour
    {
        [SerializeField] private Text _text;

        private int _currentCoins = 0;

        private void Start()
        {
            StatsManager.Inst.OnCoinsChanged += OnCoinsChangedHandler;

            OnCoinsChangedHandler(StatsManager.Inst.CoinsCount);
        }

        private void OnCoinsChangedHandler(int coins)
        {
            _currentCoins = coins;
            _text.text = coins.ToString();
        }
    }
}
