using KnifeGame.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeGame.UI
{
    public class SwipeDisplay : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameObject _start;
        [SerializeField] private List<GameObject> _dots;
        [SerializeField] private GameObject _end;

        private Vector3 _swipeStart;

        private void Start()
        {
            SwipeManager.Inst.OnSwipeStart += OnSwipeStartHandler;
            SwipeManager.Inst.OnSwipeDrag += OnSwipeDragHandler;
            SwipeManager.Inst.OnSwipeEnd += (pos) => Disable();
        }

        private Vector3 ConvertPos(Vector2 pos)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, pos, _canvas.worldCamera, out pos);
            return _canvas.transform.TransformPoint(pos);
        }

        private void Enable()
        {
            _start.SetActive(true);
            _end.SetActive(true);
            _dots.ForEach((item) => item.SetActive(true));
        }
        private void Disable()
        {
            _start.SetActive(false);
            _end.SetActive(false);
            _dots.ForEach((item) => item.SetActive(false));
        }

        private void OnSwipeStartHandler(Vector2 start)
        {
            Enable();
            OnSwipeDragHandler(start);

            _swipeStart = ConvertPos(start);
            _start.transform.position = _swipeStart;
        }

        private void OnSwipeDragHandler(Vector2 pos)
        {
            var dragPos = ConvertPos(pos);
            _end.transform.position = dragPos;

            Vector3 dist = dragPos - _swipeStart;
            for (int i = 0; i < _dots.Count; i++)
            {
                float percent = (float)i / (float)_dots.Count;
                var dot = _dots[i];
                dot.transform.position = dist * percent + _swipeStart;
            }
        }
    }
}
