using KnifeGame.Managers;
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

        private RectTransform _start;
        private RectTransform _end;
        private List<RectTransform> _dots;
        private Vector3 _swipeStart;

        private void Awake()
        {
            _dots = new List<RectTransform>();
            for (int i = 0; i < _dotsCount; i++)
            {
                _dots.Add(
                    Instantiate(_middlePrefab, transform).GetComponent<RectTransform>()
                 );
            }
            _start = Instantiate(_startPrefab, transform).GetComponent<RectTransform>();
            _end = Instantiate(_endPrefab, transform).GetComponent<RectTransform>();

            Disable();
        }

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
            _start.gameObject.SetActive(true);
            _end.gameObject.SetActive(true);
            _dots.ForEach((item) => item.gameObject.SetActive(true));
        }
        private void Disable()
        {
            _start.gameObject.SetActive(false);
            _end.gameObject.SetActive(false);
            _dots.ForEach((item) => item.gameObject.SetActive(false));
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
    }
}
