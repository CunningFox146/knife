using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeGame.UI
{
    public class Guide : MonoBehaviour
    {
        [SerializeField] private Vector3 _endPos;
        [SerializeField] private Vector3 _endRot;

        private Image _image;
        private Sequence _sequence;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Start()
        {
            var clearColor = new Color(1, 1, 1, 0);
            var rectTrans = transform as RectTransform;
            var startRot = rectTrans.eulerAngles;

            _image.color = clearColor;

            _sequence = DOTween.Sequence()
                .SetDelay(1f)
                .SetLoops(-1)
                /*TIMELINE*/

                .Append(rectTrans.DORotate(_endRot, 0.35f).SetEase(Ease.OutCubic))
                .Join(_image.DOColor(Color.white, 0.35f).SetEase(Ease.OutCubic))

                .Append(rectTrans.DOAnchorPos(_endPos, 1f).SetEase(Ease.InOutSine))

                .Append(rectTrans.DORotate(startRot, 0.35f).SetEase(Ease.InCubic))
                .Join(_image.DOColor(clearColor, 0.35f).SetEase(Ease.InCubic));
        }

        private void OnDestroy()
        {
            if (_sequence != null)
            {
                _sequence.Kill();
                _sequence = null;
            }
        }

        public void Kill()
        {
            OnDestroy();
            _image.DOColor(new Color(1, 1, 1, 0), 0.35f).SetEase(Ease.InCubic).OnComplete(()=>Destroy(gameObject));
        }
    }
}
