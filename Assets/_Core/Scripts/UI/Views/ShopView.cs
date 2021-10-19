using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using KnifeGame.Managers;
using UnityEngine;

namespace KnifeGame.UI.Views
{
    public class ShopView : View
    {
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTrans;

        private Sequence _animSequence;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTrans = (RectTransform)transform;
        }

        private void Start()
        {
            Show();
        }

        public override void Show()
        {
            base.Show();

            KillSequence();

            float duration = 0.25f;
            _canvasGroup.alpha = 0f;
            _rectTrans.anchoredPosition = Vector2.up * 50f;

            _animSequence = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(1f, duration))
                .Join(_rectTrans.DOAnchorPos(Vector2.zero, duration))
                .SetEase(Ease.OutCubic);
        }

        public override void Hide()
        {
            KillSequence();

            float duration = 0.25f;

            _animSequence = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(0f, duration))
                .Join(_rectTrans.DOAnchorPos(Vector2.up * 50f, duration))
                .SetEase(Ease.OutCubic)
                .OnComplete(()=>base.Hide());
        }

        private void KillSequence()
        {
            if (_animSequence != null)
            {
                _animSequence.Kill();
                _animSequence = null;
            }
        }
    }
}
