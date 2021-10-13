using DG.Tweening;
using KnifeGame.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeGame.UI.Views
{
    public class MainView : View
    {
        [SerializeField] private RectTransform _stats;
        [SerializeField] private Guide _guide;
        [SerializeField] private Text _title;
        [SerializeField] private Text _score;
        [SerializeField] private Text _scoreMax;
        [SerializeField] private Text _coins;
        [SerializeField] private GameObject _flipPrefab;

        private Vector2 _statsPos;
        private bool _titleVisible = true;

        private void Start()
        {
            ShowTitle();

            SwipeManager.Inst.OnSwipeStart += OnSwipeStartHandler;
            ScoreManager.Inst.OnKnifeFlip += OnKnifeFlipHandler;
        }

        private void OnKnifeFlipHandler(int points)
        {
            Instantiate(_flipPrefab, transform).GetComponent<FlipIndicator>().Init(points);
        }

        private void OnSwipeStartHandler(Vector2 start)
        {
            if (_titleVisible)
            {
                _titleVisible = false;
                HideTitle();
            }
        }

        private void ShowTitle()
        {
            var titleScale = _title.rectTransform.localScale;
            _title.rectTransform.localScale = new Vector3(titleScale.x, 0f, titleScale.z);
            _title.rectTransform.DOScale(titleScale, 1f).SetEase(Ease.OutBounce);

            _statsPos = _stats.anchoredPosition;
            _stats.anchoredPosition = _statsPos + Vector2.up * 300f;
            _stats.gameObject.SetActive(false);
        }

        private void HideTitle()
        {
            _stats.gameObject.SetActive(true);
            _stats.DOAnchorPos(_statsPos, 0.5f).SetEase(Ease.OutBack);

            var titleScale = _title.rectTransform.localScale;
            _title.rectTransform.DOScale(new Vector3(titleScale.x, 0f, titleScale.z), 0.5f)
                .SetEase(Ease.InBack)
                .OnComplete(() => _title.gameObject.SetActive(false));

            _guide.Kill();
        }
    }
}
