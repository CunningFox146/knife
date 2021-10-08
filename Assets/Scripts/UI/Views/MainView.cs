using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeGame.UI.Views
{
    public class MainView : View
    {
        [SerializeField] private RectTransform _stats;
        [SerializeField] private GameObject _guide;
        [SerializeField] private Text _title;
        [SerializeField] private Text _score;
        [SerializeField] private Text _scoreMax;
        [SerializeField] private Text _coins;

        private Vector2 _statsPos;

        private void Start()
        {
            ShowTitle();
        }

        private void ShowTitle()
        {
            var titleScale = _title.rectTransform.localScale;
            _title.rectTransform.localScale = new Vector3(titleScale.x, 0f, titleScale.z);
            _title.rectTransform.DOScale(titleScale, 0.5f).SetEase(Ease.OutBounce);

            _statsPos = _stats.anchoredPosition;
            _stats.anchoredPosition = _statsPos + Vector2.up * 300f;
        }

        private void HideTitle()
        {
            _stats.DOAnchorPos(_statsPos, 0.5f);
            _title.rectTransform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBounce);

            Destroy(_guide);
        }
    }
}
