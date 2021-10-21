using DG.Tweening;
using KnifeGame.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeGame.UI
{
    public class FlipIndicator : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] public Text _text;

        private CanvasGroup _canvasGroup;
        private Sequence _animSequence;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        
        public void OnFlip(int points, Text score)
        {
            _text.text = points.ToString();

            var rectTrans = (RectTransform)transform;

            rectTrans.localScale = Vector3.one * 1.5f;
            _canvasGroup.alpha = 0f;

            _animSequence = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(1f, 0.5f).SetEase(Ease.OutCubic))
                .Join(rectTrans.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce))
                .Join(
                    ((RectTransform)_image.transform).DORotate(new Vector3(0f, 0f, 360f), 0.75f, RotateMode.FastBeyond360)
                    .SetEase(Ease.OutCirc)
                )
                .Append(
                    _canvasGroup.DOFade(0f, 0.5f)
                    .OnStart(() => AnimateText(score))
                    .OnComplete(() => Destroy(gameObject))
                );
        }

        private void AnimateText(Text score)
        {
            if (score != null) return;

            var scoreRect = (RectTransform)score.transform;
            _text.transform.SetParent(score.transform);

            var color = _text.color;
            color.a = 0f;
            _text.DOColor(color, 0.25f)
                .SetDelay(0.25f)
                .SetEase(Ease.OutCirc);
            ((RectTransform)_text.transform).DOAnchorPos(new Vector2(score.preferredWidth * -1.5f, 0f), 0.5f)
                .SetEase(Ease.OutSine)
                .OnComplete(() => Destroy(_text.gameObject));
        }
    }
}
