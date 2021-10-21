using DG.Tweening;
using KnifeGame.Knife;
using KnifeGame.Managers;
using KnifeGame.Util;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeGame.UI.Views
{
    public class MainView : View
    {
        [SerializeField] private Camera _uiCamera;

        [SerializeField] private GameObject _flipPrefab;

        [SerializeField] private RectTransform _menu;
        [SerializeField] private RectTransform _stats;
        [SerializeField] private Guide _guide;

        [SerializeField] private Text _score;

        private Vector2 _statsPos;
        private Vector2 _menuPos;
        private bool _statsHidden = true;

        private void Start()
        {
            HideStats();

            GameManager.Inst.OnGameStart += OnGameStartHandler;
            StatsManager.Inst.OnKnifeFlip += OnKnifeFlipHandler;
        }

        private void OnKnifeFlipHandler(KnifeController knife, int points)
        {
            if (!SwipeManager.Inst.IsActive) return;

            // I don't know why it works, but it does
            Vector3 screenPoint = _uiCamera.WorldToScreenPoint(knife.transform.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), screenPoint, _uiCamera, out Vector2 result);

            if (_flipPrefab != null)
            {
                var indicator = Instantiate(_flipPrefab, transform);
                ((RectTransform)indicator.transform).anchoredPosition = result;
                indicator.GetComponent<FlipIndicator>().OnFlip(points, _score);
            }
        }

        private void OnGameStartHandler()
        {
            GameManager.Inst.OnGameStart -= OnGameStartHandler;

            if (_statsHidden)
            {
                _statsHidden = false;
                ShowStats();
            }
        }

        private void HideStats()
        {
            _statsPos = _stats.anchoredPosition;
            _menuPos = _menu.anchoredPosition;

            _stats.anchoredPosition = _statsPos + Vector2.up * 300f;
            _stats.gameObject.SetActive(false);

            _menu.gameObject.SetActive(false);
            _menu.anchoredPosition = Vector2.down * 500f;
        }

        private void ShowStats()
        {
            _stats.gameObject.SetActive(true);
            _stats.DOAnchorPos(_statsPos, 0.5f).SetEase(Ease.OutBack);

            _menu.gameObject.SetActive(true);
            _menu.DOAnchorPos(_menuPos, 0.5f).SetEase(Ease.OutCubic);

            _guide.Kill();
        }
    }
}
