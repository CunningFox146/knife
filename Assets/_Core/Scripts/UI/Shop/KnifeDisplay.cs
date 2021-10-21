using DG.Tweening;
using KnifeGame.Util;
using UnityEngine;

namespace KnifeGame.UI.Shop
{
    public class KnifeDisplay : MonoBehaviour
    {
        [SerializeField] private RectTransform _modelContainer;
        [SerializeField] private Transform _model;

        private Tween _modelAppearTween;

        public void Start()
        {
            _modelContainer.DORotate(Vector3.up * 360f, 5f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }

        public void SetModel(GameObject modelPrefab)
        {
            if (_model != null)
            {
                Destroy(_model.gameObject);
            }

            _model = Instantiate(modelPrefab, _modelContainer).transform;
            _model.SetLayerInChildren(LayerMask.NameToLayer("UI"));
            _model.eulerAngles = Vector3.forward * -30f;
            _model.localScale = Vector3.zero;

            _modelAppearTween?.Kill();
            _modelAppearTween = _model.DOScale(Vector3.one * 125f, 0.5f)
                .SetEase(Ease.OutBack);
        }
    }
}
