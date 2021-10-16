using DG.Tweening;
using KnifeGame.Managers;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class CoinDisplay : MonoBehaviour
    {
        [SerializeField] private Text _text;

        private CanvasGroup _canvasGroup;
        private int _currentCoins = 0;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;
        }

        private void Start()
        {
            GameManager.Inst.OnGameStart += OnGameStartHandler;
            ShopManager.Inst.OnCoinsChanged += OnCoinsChangedHandler;

            OnCoinsChangedHandler(ShopManager.Inst.CoinsCount);
        }

        private void OnCoinsChangedHandler(int coins)
        {
            _currentCoins = coins;
            _text.text = coins.ToString();
        }

        private void OnGameStartHandler()
        {
            GameManager.Inst.OnGameStart -= OnGameStartHandler;
            _canvasGroup.DOFade(1f, 0.5f).SetEase(Ease.OutCubic);
        }
    }
}
