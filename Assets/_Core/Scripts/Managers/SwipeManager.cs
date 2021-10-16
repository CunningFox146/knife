using KnifeGame.Util;
using System;
using UnityEngine;

namespace KnifeGame.Managers
{
    public class SwipeManager : Singleton<SwipeManager>
    {
        private readonly bool IsMobile = Application.isMobilePlatform;

        public event Action<Vector2> OnSwipeStart;
        public event Action<Vector2> OnSwipeDrag;
        public event Action<Vector2> OnSwipeEnd;
        public event Action<Vector3> OnSwipe;

        private Vector2 _swipeStart;
        private Vector2 _swipeCurrent;
        private bool _isSwiping = false;

        private Vector2 TouchPosition => (Vector2)Input.mousePosition;

        private void Update()
        {
            Swipe();
        }

        private void Swipe()
        {
            if (Input.GetMouseButtonUp(0) && _isSwiping)
            {
                var camera = Camera.main;

                OnSwipeEnd?.Invoke(_swipeCurrent);
                OnSwipe?.Invoke(camera.ScreenToViewportPoint(_swipeCurrent) - camera.ScreenToViewportPoint(_swipeStart));
                _isSwiping = false;
            }

            if (!Input.GetMouseButton(0)) return;

            Vector2 swipeOld = _swipeCurrent;
            _swipeCurrent = TouchPosition;

            if (!_isSwiping)
            {
                _isSwiping = true;
                _swipeStart = _swipeCurrent;
                OnSwipeStart?.Invoke(_swipeCurrent);
            }
            else if (swipeOld != _swipeCurrent)
            {
                OnSwipeDrag?.Invoke(_swipeCurrent);
            }
        }
    }
}
