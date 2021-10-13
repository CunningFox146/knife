using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeGame.UI
{
    public class FlipIndicator : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] public Text _text;

        private Sequence _animSequence;

        public void Init(int points)
        {
            _text.text = points.ToString();

            var startColor = _image.color;
            var blank = new Color(startColor.r, startColor.g, startColor.b, 0f);
            var rectTrans = (RectTransform)transform;

            rectTrans.localScale = Vector3.one * 1.5f;
            _image.color = blank;

            _animSequence = DOTween.Sequence()
                .Append(_image.DOColor(startColor, 0.5f).SetEase(Ease.OutCubic))
                .Join(rectTrans.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce))
                .Join(
                    ((RectTransform)_image.transform).DORotate(new Vector3(0f, 0f, 360f), 0.75f, RotateMode.FastBeyond360)
                    .SetEase(Ease.OutCirc)
                )
                .Append(
                    _image.DOColor(blank, 0.5f)
                    .OnComplete(() => Destroy(gameObject))
                );
        }
    }
}
