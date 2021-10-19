using DG.Tweening;
using KnifeGame.Managers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeGame.UI
{
    public class SwipeDisplay : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameObject _startPrefab;
        [SerializeField] private GameObject _middlePrefab;
        [SerializeField] private GameObject _endPrefab;
        [SerializeField] private int _dotsCount = 5;

        private CanvasGroup _canvasGroup;
        private RectTransform _start;
        private RectTransform _end;
        private List<RectTransform> _dots;
        private Vector3 _swipeStart;
        private bool _eventsActive;

        private Tween _fadeTween;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            RectTransform GetRectTransform(GameObject obj) => (RectTransform)obj.transform;

            _dots = new List<RectTransform>();
            for (int i = 0; i < _dotsCount; i++)
            {
                _dots.Add(
                    GetRectTransform(Instantiate(_middlePrefab, transform))
                 );
            }
            _start = GetRectTransform(Instantiate(_startPrefab, transform));
            _end = GetRectTransform(Instantiate(_endPrefab, transform));

            Disable(false);
        }

        private void Start()
        {
            SwipeManager.Inst.OnActiveChanged += OnActiveChangedHandler;

            EnableEvents();
        }

        private void OnActiveChangedHandler(bool isActive)
        {
            if (isActive == _eventsActive) return;
            if (isActive)
            {
                EnableEvents();
            }
            else
            {
                Disable(false);
                DisableEvents();
            }
        }

        private void EnableEvents()
        {
            _eventsActive = true;
            SwipeManager.Inst.OnSwipeStart += OnSwipeStartHandler;
            SwipeManager.Inst.OnSwipeDrag += OnSwipeDragHandler;
            SwipeManager.Inst.OnSwipeEnd += OnSwipeEndHandler;
        }


        private void DisableEvents()
        {
            _eventsActive = false;
            SwipeManager.Inst.OnSwipeStart -= OnSwipeStartHandler;
            SwipeManager.Inst.OnSwipeDrag -= OnSwipeDragHandler;
            SwipeManager.Inst.OnSwipeEnd -= OnSwipeEndHandler;
        }

        private Vector3 ConvertPos(Vector2 pos)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, pos, _canvas.worldCamera, out pos);
            return _canvas.transform.TransformPoint(pos);
        }

        private void Enable()
        {
            KillTween();

            _canvasGroup.alpha = 1f;
            _start.gameObject.SetActive(true);
            _end.gameObject.SetActive(true);
            _dots.ForEach((item) => item.gameObject.SetActive(true));
        }

        private void Disable(bool isTweening = true)
        {
            KillTween();

            void OnComplete()
            {
                _start.gameObject.SetActive(false);
                _end.gameObject.SetActive(false);
                _dots.ForEach((item) => item.gameObject.SetActive(false));
            }

            if (isTweening)
            {
                _fadeTween = _canvasGroup.DOFade(0f, 0.25f)
                    .SetEase(Ease.InSine)
                    .OnComplete(OnComplete);
            }
            else
            {
                _canvasGroup.alpha = 0f;
                OnComplete();
            }
        }

        private void KillTween()
        {
            if (_fadeTween != null)
            {
                _fadeTween.Kill();
                _fadeTween = null;
            }
        }

        private void OnSwipeStartHandler(Vector2 start)
        {
            Enable();

            _swipeStart = ConvertPos(start);
            _start.position = _swipeStart;

            OnSwipeDragHandler(start);
        }

        private void OnSwipeDragHandler(Vector2 pos)
        {
            var dragPos = ConvertPos(pos);
            _end.position = dragPos;

            Vector3 dist = dragPos - _swipeStart;
            for (int i = 0; i < _dotsCount; i++)
            {
                float percent = i / (float)_dotsCount;
                var dot = _dots[i];
                dot.position = dist * percent + _swipeStart;
                dot.localScale = Vector3.one * (0.5f + percent * 0.5f);
            }
        }

        private void OnSwipeEndHandler(Vector2 obj)
        {
            Disable();
        }
    }
}
