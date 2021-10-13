using DG.Tweening;
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

        [SerializeField] private RectTransform _stats;
        [SerializeField] private Guide _guide;

        [SerializeField] private Text _score;
        [SerializeField] private Text _scoreMax;
        [SerializeField] private Text _coins;

        private Vector2 _statsPos;
        private bool _statsHidden = true;

        private void Start()
        {
            HideStats();

            SwipeManager.Inst.OnSwipeStart += OnSwipeStartHandler;
            ScoreManager.Inst.OnKnifeFlip += OnKnifeFlipHandler;
        }

        private void OnKnifeFlipHandler(GameObject knife, int points)
        {
            // I don't know why it works, but it does
            Vector3 screenPoint = _uiCamera.WorldToScreenPoint(knife.transform.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), screenPoint, _uiCamera, out Vector2 result);

            var indicator = Instantiate(_flipPrefab, transform);
            ((RectTransform)indicator.transform).anchoredPosition = result;
            indicator.GetComponent<FlipIndicator>().Init(points, _score);
        }

        private void OnSwipeStartHandler(Vector2 start)
        {
            if (_statsHidden)
            {
                _statsHidden = false;
                ShowStats();
            }
        }

        private void HideStats()
        {
            _statsPos = _stats.anchoredPosition;
            _stats.anchoredPosition = _statsPos + Vector2.up * 300f;
            _stats.gameObject.SetActive(false);
        }

        private void ShowStats()
        {
            _stats.gameObject.SetActive(true);
            _stats.DOAnchorPos(_statsPos, 0.5f).SetEase(Ease.OutBack);

            _guide.Kill();
        }
    }
}
