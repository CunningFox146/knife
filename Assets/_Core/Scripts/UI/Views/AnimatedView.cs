using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using KnifeGame.Managers;
using UnityEngine;

namespace KnifeGame.UI.Views
{
    public class AnimatedView : View
    {
        protected CanvasGroup _canvasGroup;
        protected RectTransform _rectTrans;

        protected Sequence _animSequence;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTrans = (RectTransform)transform;
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

        //public override void Hide()
        //{
        //    KillSequence();

        //    float duration = 0.25f;

        //    _animSequence = DOTween.Sequence()
        //        .Append(_canvasGroup.DOFade(0f, duration))
        //        .Join(_rectTrans.DOAnchorPos(Vector2.up * 50f, duration))
        //        .SetEase(Ease.OutCubic)
        //        .OnComplete(()=>base.Hide());
        //}

        protected void KillSequence()
        {
            if (_animSequence != null)
            {
                _animSequence.Kill();
                _animSequence = null;
            }
        }
    }
}
