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

        private Vector2 _swipeStart;
        private Vector2 _swipeCurrent;
        private bool _isSwiping = false;

        private bool IsTouching => IsMobile ? Input.touchCount > 0 : Input.GetMouseButton(0);
        private Vector2 TouchPosition => IsMobile ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;

        private void Update()
        {
            Swipe();
        }

        private TouchPhase GetPhase()
        {
            if (IsMobile) return Input.GetTouch(0).phase;
            if (Input.GetMouseButtonDown(0)) return TouchPhase.Began;
            if (Input.GetMouseButtonUp(0)) return TouchPhase.Ended;
            return TouchPhase.Moved;
        }

        private void Swipe()
        {
            TouchPhase phase = GetPhase();

            if (phase == TouchPhase.Ended)
            {
                OnSwipeEnd?.Invoke(_swipeCurrent);
                _isSwiping = false;
                Debug.LogWarning($"End:{_swipeCurrent}");
            }

            if (!IsTouching) return;

            Vector2 swipeOld = _swipeCurrent;
            _swipeCurrent = TouchPosition;

            if (!_isSwiping)
            {
                _isSwiping = true;
                OnSwipeStart?.Invoke(_swipeCurrent);
                Debug.LogError($"Start:{_swipeCurrent}");
            }
            else if (swipeOld != _swipeCurrent)
            {
                OnSwipeDrag?.Invoke(_swipeCurrent);
                Debug.Log($"Drag:{_swipeCurrent}");
            }
        }
    }
}
